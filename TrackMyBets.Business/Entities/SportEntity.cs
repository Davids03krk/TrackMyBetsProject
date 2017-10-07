using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class SportEntity
    {
        #region Attributes
        public int IdSport { get; set; }
        public string DescSport { get; set; }
        public float? DurationMatchInHours { get; set; }
        #endregion
        
        #region Public Methods

        /// <summary>
        /// Method that returns a list with all the sport.
        /// </summary>
        /// <returns></returns>
        public static List<SportEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var sports = new List<SportEntity>();

                dbContext.Sport.ToList().ForEach(x => sports.Add(SportEntity.Load(x.IdSport)));

                return sports;
            }
        }

        /// <summary>
        /// Method that returns the sport with the Id passed as parameter. 
        /// </summary>
        /// <param name="sportId"></param>
        /// <returns></returns>
        public static SportEntity Load(int sportId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbSport = dbContext.Sport.Find(sportId);

                if (dbSport == null)
                    return null;

                return MapFromBD(dbSport);
            }
        }

        /// <summary>
        /// Method that adds to the database the past sport as a parameter.
        /// </summary>
        /// <param name="sport"></param>
        public static void Create(SportEntity sport)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                if (sport.Exist())
                    throw new DuplicatedSportException(sport.ToString());

                var dbSport = sport.MapToBD();
                dbContext.Sport.Add(dbSport);
                dbContext.SaveChanges();

                sport.IdSport = dbSport.IdSport;
            }
        }

        /// <summary>
        /// Method that updates the current sport database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbSport = dbContext.Sport.Find(IdSport);

                if (dbSport == null)
                    throw new NotFoundSportException(IdSport.ToString());

                dbSport.DescSport = DescSport;
                dbSport.DurationMatchInHours = DurationMatchInHours;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current sport.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbSport = dbContext.Sport.Find(IdSport);

                if (dbSport == null)
                    throw new NotFoundSportException(IdSport.ToString());

                TeamEntity.Load(this).ForEach(x => x.Delete());
                EventEntity.Load(this).ForEach(x => x.Delete());

                dbContext.Sport.Remove(dbSport);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current sport as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Sport[ {0} ]", DescSport);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current sport exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                return dbContext.Sport.Any(x => x.DescSport == DescSport);
            }
        }

        /// <summary>
        /// Method that maps a sport to the database model.
        /// </summary>
        /// <returns></returns>
        internal Sport MapToBD()
        {
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
        internal static SportEntity MapFromBD(Sport dbSport)
        {
            var sportEntity = new SportEntity()
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
