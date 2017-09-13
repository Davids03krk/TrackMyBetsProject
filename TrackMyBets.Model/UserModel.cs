using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class UserModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public UserModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdUser { get; set; }

        [Required]
        public string Nick { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string SurnameFirst { get; set; }

        public string SurnameSecond { get; set; }

        [Required]
        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Direccion { get; set; }
        #endregion

        #region Public Methods
        public static UserModel FromEntity(UserEntity user)
        {
            UserModel userModel = new UserModel(_dbContext)
            {
                IdUser = user.IdUser,
                Nick = user.Nick,
                Password = user.Password,
                Name = user.Name,
                SurnameFirst = user.SurnameFirst,
                SurnameSecond = user.SurnameSecond,
                Email = user.Email,
                Telefono = user.Telefono,
                Direccion = user.Direccion
            };

            return userModel;
        }

        public UserEntity ToEntities()
        {
            var userEntity = new UserEntity(_dbContext)
            {
                IdUser = IdUser,
                Nick = Nick,
                Password = Password,
                Name = Name,
                SurnameFirst = SurnameFirst,
                SurnameSecond = SurnameSecond,
                Email = Email,
                Telefono = Telefono,
                Direccion = Direccion
            };

            return userEntity;
        }
        #endregion
    }
}
