using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class SportModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public SportModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdSport { get; set; }

        [Required]
        public string DescSport { get; set; }

        public float? DurationMatchInHours { get; set; }
        #endregion

        #region Public Methods
        public static SportModel FromEntity(SportEntity sport)
        {
            SportModel sportModel = new SportModel(_dbContext)
            {
                IdSport = sport.IdSport,
                DescSport = sport.DescSport,
                DurationMatchInHours = sport.DurationMatchInHours
            };

            return sportModel;
        }

        public SportEntity ToEntities()
        {
            var sportEntity = new SportEntity(_dbContext)
            {
                IdSport = IdSport,
                DescSport = DescSport,
                DurationMatchInHours = DurationMatchInHours
            };

            return sportEntity;
        }
        #endregion
    }
}
