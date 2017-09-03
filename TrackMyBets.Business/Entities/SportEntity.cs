using System;
using System.Collections.Generic;
using System.Text;
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
        public static SportEntity Load(int sportId) {
            var dbSport = _dbContext.Sport.Find(sportId);

            if (dbSport == null)
                return null;

            return MapFromBD(dbSport);
        }

        public static List<SportEntity> Load() {
            var sports = new List<SportEntity>();

            _dbContext.Sport.ToList().ForEach(x => sports.Add(SportEntity.Load(x.IdSport)));

            return sports;
        }

        public static void Create(SportEntity sport) {
            if (sport.Exist())
                throw new DuplicatedSportException(sport.DescSport);
            
            var dbSport = sport.MapToBD();
            _dbContext.Sport.Add(dbSport);
            _dbContext.SaveChanges();

            sport.IdSport = dbSport.IdSport;
        }

        public void Update() {
            var dbSport = _dbContext.Sport.Find(IdSport);

            if (dbSport == null)
                throw new NotFoundSportException(IdSport.ToString());

            dbSport.IdSport = IdSport;
            dbSport.DescSport = DescSport;
            dbSport.DurationMatchInHours = DurationMatchInHours;

            _dbContext.SaveChanges();
        }

        public void Delete() {
            var dbSport = _dbContext.Sport.Find(IdSport);

            if (dbSport == null)
                throw new NotFoundSportException(IdSport.ToString());

            //aqui es necesario eliminar los registros que hacen referencia ha este sport
            //TeamEntity.Load(this).ForEach(x => x.Delete());
            //EventEntity.Load(this).ForEach(x => x.Delete());

            _dbContext.Sport.Remove(dbSport);
            _dbContext.SaveChanges();
        }

        public override string ToString() {
            return string.Format("Sport[ {0} ]", DescSport);
        }
        #endregion

        #region Internal Methods        
        internal bool Exist() {
            return _dbContext.Sport.Any(x => x.DescSport == DescSport);
        }

        internal Sport MapToBD() {
            var dbSport = new Sport();

            dbSport.DescSport = DescSport;
            dbSport.DurationMatchInHours = DurationMatchInHours;

            return dbSport;
        }

        internal static SportEntity MapFromBD(Sport dbSport) {
            var sportEntity = new SportEntity(_dbContext);

            sportEntity.IdSport = dbSport.IdSport;
            sportEntity.DescSport = dbSport.DescSport;
            sportEntity.DurationMatchInHours = dbSport.DurationMatchInHours;

            return sportEntity;
        }
        #endregion
    }
}
