using System;
using System.Collections.Generic;
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

        //[Required]
        //public BetModel Bet { get; set; }

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
                //Bet = BetModel.FromEntity(BetEntity.Load(pick.IdBet)),
                Event = EventModel.FromEntity(EventEntity.Load(pick.IdEvent)),
                TypePickValue = (Enumerators.PickTypes)Enum.Parse(typeof(Enumerators.PickTypes), pick.IdPickType.ToString())
            };


            // TODO. Refactorizar esto.
            if (pickModel.TypePickValue == Enumerators.PickTypes.WINNER)
                pickModel.TypePickData = TypePickWinnerModel.FromEntity(TypePickWinnerEntity.Load(pick.IdPick));
            else if (pickModel.TypePickValue == Enumerators.PickTypes.TOTAL_POINTS)
                pickModel.TypePickData = TypePickTotalPointsModel.FromEntity(TypePickTotalPointsEntity.Load(pick.IdPick));

            return pickModel;
        }

        public PickEntity ToEntities()
        {
            var pickEntity = new PickEntity(_dbContext);

            pickEntity.IdPick = this.IdPick;
            //pickEntity.IdBet = this.Bet.IdBet;
            pickEntity.IdEvent = this.Event.IdEvent;
            pickEntity.IdPickType = (int)this.TypePickData.TypePick;

            return pickEntity;
        }
        #endregion
    }
}
