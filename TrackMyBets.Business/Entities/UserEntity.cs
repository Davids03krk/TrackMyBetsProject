using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class UserEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdUser { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurnameFirst { get; set; }
        public string SurnameSecond { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        #endregion

        #region Constructor
        public UserEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method that returns a list with all the user.
        /// </summary>
        /// <returns></returns>
        public static List<UserEntity> Load()
        {
            var users = new List<UserEntity>();

            _dbContext.User.ToList().ForEach(x => users.Add(UserEntity.Load(x.IdUser)));

            return users;
        }

        /// <summary>
        /// Method that returns the user with the Id passed as parameter. 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserEntity Load(int userId)
        {
            var dbUser = _dbContext.User.Find(userId);

            if (dbUser == null)
                return null;

            return MapFromBD(dbUser);
        }

        /// <summary>
        /// Method that adds to the database the past user as a parameter.
        /// </summary>
        /// <param name="user"></param>
        public static void Create(UserEntity user)
        {
            if (user.Exist())
                throw new DuplicatedUserException(user.ToString());

            var dbUser = user.MapToBD();
            _dbContext.User.Add(dbUser);
            _dbContext.SaveChanges();

            user.IdUser = dbUser.IdUser;
        }

        /// <summary>
        /// Method that updates the current user database.
        /// </summary>
        public void Update()
        {
            var dbUser = _dbContext.User.Find(IdUser);

            if (dbUser == null)
                throw new NotFoundUserException(IdUser.ToString());

            dbUser.Nick = Nick;
            dbUser.Password = Password;
            dbUser.Name = Name;
            dbUser.SurnameFirst = SurnameFirst;
            dbUser.SurnameSecond = SurnameSecond;
            dbUser.Email = Email;
            dbUser.Telefono = Telefono;
            dbUser.Direccion = Direccion;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current user.
        /// </summary>
        public void Delete()
        {
            var dbUser = _dbContext.User.Find(IdUser);

            if (dbUser == null)
                throw new NotFoundUserException(IdUser.ToString());

            RelUserBookmakerEntity.Load(this).ForEach(x => x.Delete());

            _dbContext.User.Remove(dbUser);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that returns the current user as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("User[ {0} ]", Nick);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current user exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist()
        {
            return _dbContext.User.Any(x => x.Nick == Nick);
        }

        /// <summary>
        /// Method that maps a user to the database model.
        /// </summary>
        /// <returns></returns>
        internal User MapToBD()
        {
            var dbUser = new User
            {
                Nick = Nick,
                Password = Password,
                Name = Name,
                SurnameFirst = SurnameFirst,
                SurnameSecond = SurnameSecond,
                Email = Email,
                Telefono = Telefono,
                Direccion = Direccion
            };

            return dbUser;
        }

        /// <summary>
        /// Method that maps a user from a database model passed by parameter.
        /// </summary>
        /// <param name="dbUser"></param>
        /// <returns></returns>
        internal static UserEntity MapFromBD(User dbUser)
        {
            var userEntity = new UserEntity(_dbContext)
            {
                IdUser = dbUser.IdUser,
                Nick = dbUser.Nick,
                Password = dbUser.Password,
                Name = dbUser.Name,
                SurnameFirst = dbUser.SurnameFirst,
                SurnameSecond = dbUser.SurnameSecond,
                Email = dbUser.Email,
                Telefono = dbUser.Telefono,
                Direccion = dbUser.Direccion
            };

            return userEntity;
        }
        #endregion
    }
}
