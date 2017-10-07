using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Business.Functions;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class TypePickTotalPointsModel : TypePickModel
    {
        #region Properties

        public bool? IsOver { get; set; }

        public bool? IsUnder { get; set; }

        [Required]
        public float ValueTotalPoints { get; set; }
        #endregion

        #region Public Methods
        public static TypePickTotalPointsModel FromEntity(TypePickTotalPointsEntity typePickTotalPoints)
        {
            TypePickTotalPointsModel typePickTotalPointsModel = new TypePickTotalPointsModel()
            {
                IdPick = typePickTotalPoints.IdPick,
                IsOver = typePickTotalPoints.IsOver,
                IsUnder = typePickTotalPoints.IsUnder,
                ValueTotalPoints = typePickTotalPoints.ValueTotalPoints,
                TypePick = Enumerators.PickTypes.TOTAL_POINTS
            };

            return typePickTotalPointsModel;
        }

        public TypePickTotalPointsEntity ToEntities()
        {
            var typePickTotalPointsEntity = new TypePickTotalPointsEntity()
            {
                IdPick = IdPick,
                IsOver = IsOver,
                IsUnder = IsUnder,
                ValueTotalPoints = ValueTotalPoints
            };

            return typePickTotalPointsEntity;
        }
        #endregion
    }
}
