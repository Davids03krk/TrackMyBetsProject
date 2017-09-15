using System;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Functions;
using System.Collections.Generic;

namespace TrackMyBets.Model
{
    public class BetModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public BetModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdBet { get; set; }

        [Required]
        public int RelUserBookmaker { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        public BookmakerModel Bookmaker { get; set; }

        public List<PickModel> Picks { get; set }

        [Required]
        public DateTime DateBet { get; set; }

        public bool? IsLiveBet { get; set; }

        public bool? IsCombinedBet { get; set; }

        [Required]
        public decimal Stake { get; set; }

        [Required]
        public float Quota { get; set; }

        public decimal? Profits { get; set; }

        public decimal? Benefits { get; set; }

        [Required]
        public Enumerators.StatusType StatusType { get; set; }
        #endregion

        #region Public Methods
        public static BetModel FromEntity(BetEntity bet)
        {
            RelUserBookmakerEntity relUserBookmakerEntity = RelUserBookmakerEntity.Load(bet.IdRelUserBookmaker);

            BetModel betModel = new BetModel(_dbContext)
            {
                IdBet = bet.IdBet,
                RelUserBookmaker = bet.IdRelUserBookmaker,
                User = UserModel.FromEntity(UserEntity.Load(relUserBookmakerEntity.IdUser)),
                Bookmaker = BookmakerModel.FromEntity(BookmakerEntity.Load(relUserBookmakerEntity.IdBookmaker)),
                DateBet = bet.DateBet,
                IsLiveBet = bet.IsLiveBet,
                IsCombinedBet = bet.IsCombinedBet,
                Stake = bet.Stake,
                Quota = bet.Quota,
                Profits = bet.Profits,
                Benefits = bet.Benefits,
                StatusType = (Enumerators.StatusType)Enum.Parse(typeof(Enumerators.StatusType), bet.IdStatusType.ToString())
            };

            PickEntity.Load(bet).ForEach(pick => {
                    betModel.Picks.Add(PickModel.FromEntity(pick));
                }
            );
            
            return betModel;
        }

        public BetEntity ToEntities()
        {
            var betEntity = new BetEntity(_dbContext)
            {
                IdBet = IdBet,
                IdRelUserBookmaker = RelUserBookmaker,
                DateBet = DateBet,
                IsLiveBet = IsLiveBet,
                IsCombinedBet = IsCombinedBet,
                Stake = Stake,
                Quota = Quota,
                Profits = Profits,
                Benefits = Benefits,
                IdStatusType = (int)StatusType
            };

            return betEntity;
        }
        #endregion
    }
}
