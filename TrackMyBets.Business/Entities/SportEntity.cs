using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class SportEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdSport { get; set; }
        public string DescSport { get; set; }
        public float? DurationMatchInHours { get; set; }
        #endregion

        #region Constructor
        public SportEntity(BD_TRACKMYBETSContext dbCOntext) {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method that returns a list with all the sport.
        /// </summary>
        /// <returns></returns>
        public static List<SportEntity> Load()
        {
            var sports = new List<SportEntity>();

            _dbContext.Sport.ToList().ForEach(x => sports.Add(SportEntity.Load(x.IdSport)));

            return sports;
        }

        /// <summary>
        /// Method that returns the sport with the Id passed as parameter. 
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns></returns>
        public static SportEntity Load(int sportId) {
            var dbSport = _dbContext.Sport.Find(sportId);

            if (dbSport == null)
                return null;

            return MapFromBD(dbSport);
        }

        /// <summary>
        /// Method that adds to the database the past sport as a parameter.
        /// </summary>
        /// <param name="sport"></param>
        public static void Create(SportEntity sport) {
            if (sport.Exist())
                throw new DuplicatedSportException(sport.ToString());
            
            var dbSport = sport.MapToBD();
            _dbContext.Sport.Add(dbSport);
            _dbContext.SaveChanges();

            sport.IdSport = dbSport.IdSport;
        }

        /// <summary>
        /// Method that updates the current sport database.
        /// </summary>
        public void Update() {
            var dbSport = _dbContext.Sport.Find(IdSport);

            if (dbSport == null)
                throw new NotFoundSportException(IdSport.ToString());

            dbSport.DescSport = DescSport;
            dbSport.DurationMatchInHours = DurationMatchInHours;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current sport.
        /// </summary>
        public void Delete() {
            var dbSport = _dbContext.Sport.Find(IdSport);

            if (dbSport == null)
                throw new NotFoundSportException(IdSport.ToString());

            TeamEntity.Load(this).ForEach(x => x.Delete());
            EventEntity.Load(this).ForEach(x => x.Delete());

            _dbContext.Sport.Remove(dbSport);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that returns the current sport as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Format("Sport[ {0} ]", DescSport);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current sport exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist() {
            return _dbContext.Sport.Any(x => x.DescSport == DescSport);
        }

        /// <summary>
        /// Method that maps a sport to the database model.
        /// </summary>
        /// <returns></returns>
        internal Sport MapToBD() {
            var dbSport = new Sport
            {
                DescSport = DescSport,
                DurationMatchInHours = DurationMatchInHours
            };

            return dbSport;
        }

        /// <summary>
        /// Method that maps a sport from a database model passed by parameter.
        /// </summary>
        /// <param name="dbSport"></param>
        /// <returns></returns>
        internal static SportEntity MapFromBD(Sport dbSport) {
            var sportEntity = new SportEntity(_dbContext)
            {
                IdSport = dbSport.IdSport,
                DescSport = dbSport.DescSport,
                DurationMatchInHours = dbSport.DurationMatchInHours
            };

            return sportEntity;
        }
        #endregion
    }
}
