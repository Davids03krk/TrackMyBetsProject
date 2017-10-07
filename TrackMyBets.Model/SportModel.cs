using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class SportModel
    {
        #region Properties
        public int IdSport { get; set; }

        [Required]
        public string DescSport { get; set; }

        public float? DurationMatchInHours { get; set; }
        #endregion

        #region Public Methods
        public static SportModel FromEntity(SportEntity sport)
        {
            SportModel sportModel = new SportModel()
            {
                IdSport = sport.IdSport,
                DescSport = sport.DescSport,
                DurationMatchInHours = sport.DurationMatchInHours
            };

            return sportModel;
        }

        public SportEntity ToEntities()
        {
            var sportEntity = new SportEntity()
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
