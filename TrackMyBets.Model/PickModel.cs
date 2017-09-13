using System;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Business.Functions;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class PickModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public PickModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdPick { get; set; }

        [Required]
        public BetModel Bet { get; set; }

        [Required]
        public EventModel Event { get; set; }

        [Required]
        public TypePickModel TypePickData { get; set; }

        [Required]
        public Enumerators.PickTypes TypePickValue { get; set; }
        #endregion

        #region Public Methods
        public static PickModel FromEntity(PickEntity pick)
        {
            PickModel pickModel = new PickModel(_dbContext)
            {
                IdPick = pick.IdPick,
                Bet = BetModel.FromEntity(BetEntity.Load(pick.IdBet)),
                Event = EventModel.FromEntity(EventEntity.Load(pick.IdEvent)),
                TypePickValue = (Enumerators.PickTypes)Enum.Parse(typeof(Enumerators.PickTypes), pick.IdPickType.ToString())
            };

            pickModel.TypePickData = GetTypePickData(pickModel.TypePickValue, pickModel.IdPick);           

            return pickModel;
        }

        public PickEntity ToEntities()
        {
            var pickEntity = new PickEntity(_dbContext)
            {
                IdPick = IdPick,
                IdBet = Bet.IdBet,
                IdEvent = Event.IdEvent,
                IdPickType = (int)TypePickValue
            };

            return pickEntity;
        }
        #endregion

        #region Internal Method
        internal static TypePickModel GetTypePickData(Enumerators.PickTypes pickType, int IdPick)
        {
            switch (pickType) {
                case Enumerators.PickTypes.WINNER:
                    return TypePickWinnerModel.FromEntity(TypePickWinnerEntity.Load(IdPick));
                case Enumerators.PickTypes.TOTAL_POINTS:
                    return TypePickTotalPointsModel.FromEntity(TypePickTotalPointsEntity.Load(IdPick));
                default:
                    return null;
            }
        }
        #endregion
    }
}
