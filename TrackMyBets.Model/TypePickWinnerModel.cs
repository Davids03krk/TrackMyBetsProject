using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Business.Functions;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class TypePickWinnerModel : TypePickModel
    {
        #region Properties
        public bool? HasHandicap { get; set; }

        public float? ValueHandicap { get; set; }

        [Required]
        public TeamModel WinnerTeam { get; set; }
        #endregion

        #region Public Methods
        public static TypePickWinnerModel FromEntity(TypePickWinnerEntity typePickWinner)
        {
            TypePickWinnerModel typePickWinnerModel = new TypePickWinnerModel()
            {
                IdPick = typePickWinner.IdPick,
                HasHandicap = typePickWinner.HasHandicap,
                ValueHandicap = typePickWinner.ValueHandicap,
                WinnerTeam = TeamModel.FromEntity(TeamEntity.Load(typePickWinner.IdWinnerTeam)),
                TypePick = Enumerators.PickTypes.WINNER
            };

            return typePickWinnerModel;
        }

        public TypePickWinnerEntity ToEntities()
        {
            var typePickWinnerEntity = new TypePickWinnerEntity()
            {
                IdPick = IdPick,
                HasHandicap = HasHandicap,
                ValueHandicap = ValueHandicap,
                IdWinnerTeam = WinnerTeam.IdTeam
            };

            return typePickWinnerEntity;
        }
        #endregion
    }
}
