using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class BookmakerEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdBookmaker { get; set; }
        public string DescBookmaker { get; set; }
        #endregion

        #region Constructor
        public BookmakerEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method that returns a list with all the bookmaker.
        /// </summary>
        /// <returns></returns>
        public static List<BookmakerEntity> Load()
        {
            var bookmakers = new List<BookmakerEntity>();

            _dbContext.Bookmaker.ToList().ForEach(x => bookmakers.Add(BookmakerEntity.Load(x.IdBookmaker)));

            return bookmakers;
        }

        /// <summary>
        /// Method that returns the bookmaker with the Id passed as parameter. 
        /// </summary>
        /// <param name="bookmakerId"></param>
        /// <returns></returns>
        public static BookmakerEntity Load(int bookmakerId)
        {
            var dbBookmaker = _dbContext.Bookmaker.Find(bookmakerId);

            if (dbBookmaker == null)
                return null;

            return MapFromBD(dbBookmaker);
        }

        /// <summary>
        /// Method that adds to the database the past dbBookmaker as a parameter.
        /// </summary>
        /// <param name="bookmaker"></param>
        public static void Create(BookmakerEntity bookmaker)
        {
            if (bookmaker.Exist())
                throw new DuplicatedBookmakerException(bookmaker.ToString());

            var dbBookmaker = bookmaker.MapToBD();
            _dbContext.Bookmaker.Add(dbBookmaker);
            _dbContext.SaveChanges();

            bookmaker.IdBookmaker = dbBookmaker.IdBookmaker;
        }

        /// <summary>
        /// Method that updates the current bookmaker database.
        /// </summary>
        public void Update()
        {
            var dbBookmaker = _dbContext.Bookmaker.Find(IdBookmaker);

            if (dbBookmaker == null)
                throw new NotFoundBookmakerException(IdBookmaker.ToString());

            dbBookmaker.DescBookmaker = DescBookmaker;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current bookmaker.
        /// </summary>
        public void Delete()
        {
            var dbBookmaker = _dbContext.Bookmaker.Find(IdBookmaker);

            if (dbBookmaker == null)
                throw new NotFoundBookmakerException(IdBookmaker.ToString());

            RelUserBookmakerEntity.Load(this).ForEach(x => x.Delete());

            _dbContext.Bookmaker.Remove(dbBookmaker);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that returns the current bookmaker as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bookmaker[ {0} ]", DescBookmaker);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current bookmaker exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist()
        {
            return _dbContext.Bookmaker.Any(x => x.DescBookmaker == DescBookmaker);
        }

        /// <summary>
        /// Method that maps a bookmaker to the database model.
        /// </summary>
        /// <returns></returns>
        internal Bookmaker MapToBD()
        {
            var dbBookmaker = new Bookmaker();

            dbBookmaker.DescBookmaker = DescBookmaker;

            return dbBookmaker;
        }

        /// <summary>
        /// Method that maps a bookmaker from a database model passed by parameter.
        /// </summary>
        /// <param name="dbBookmaker"></param>
        /// <returns></returns>
        internal static BookmakerEntity MapFromBD(Bookmaker dbBookmaker)
        {
            var bookmakerEntity = new BookmakerEntity(_dbContext);

            bookmakerEntity.IdBookmaker = dbBookmaker.IdBookmaker;
            bookmakerEntity.DescBookmaker = dbBookmaker.DescBookmaker;

            return bookmakerEntity;
        }
        #endregion
    }
}
