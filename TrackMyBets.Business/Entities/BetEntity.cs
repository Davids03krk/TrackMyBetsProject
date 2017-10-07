using System;
using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class BetEntity
    {
        #region Attributes
        public int IdBet { get; set; }
        public int IdRelUserBookmaker { get; set; }
        public DateTime DateBet { get; set; }
        public bool? IsLiveBet { get; set; }
        public bool? IsCombinedBet { get; set; }
        public decimal Stake { get; set; }
        public float Quota { get; set; }
        public decimal? Profits { get; set; }
        public decimal? Benefits { get; set; }
        public int IdStatusType { get; set; }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the bet.
        /// </summary>
        /// <returns></returns>
        public static List<BetEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var bets = new List<BetEntity>();

                dbContext.Bet.ToList().ForEach(x => bets.Add(BetEntity.Load(x.IdBet)));

                return bets;
            }
        }

        /// <summary>
        /// Method that returns the bet with the Id passed as parameter. 
        /// </summary>
        /// <param name="betId"></param>
        /// <returns></returns>
        public static BetEntity Load(int betId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbBet = dbContext.Bet.Find(betId);

                if (dbBet == null)
                    return null;

                return MapFromBD(dbBet);
            }
        }

        /// <summary>
        /// Method that returns a list of bets of a rel_user_bookmaker passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<BetEntity> Load(RelUserBookmakerEntity relUserBookmaker)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var bets = new List<BetEntity>();

                dbContext.Bet.ToList().ForEach(x =>
                {
                    if (x.IdRelUserBookmaker == relUserBookmaker.IdRelUserBookmaker)
                        bets.Add(BetEntity.Load(x.IdBet));
                });

                return bets;
            }
        }

        /// <summary>
        /// Method that adds to the database the past bet as a parameter.
        /// </summary>
        /// <param name="bet"></param>
        public static void Create(BetEntity bet)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbBet = bet.MapToBD();

                dbContext.Bet.Add(dbBet);
                dbContext.SaveChanges();

                bet.IdBet = dbBet.IdBet;
            }
        }

        /// <summary>
        /// Method that updates the current bet database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbBet = dbContext.Bet.Find(IdBet);

                if (dbBet == null)
                    throw new NotFoundBetException(IdBet.ToString());

                dbBet.DateBet = DateBet;
                dbBet.IsLiveBet = IsLiveBet;
                dbBet.IsCombinedBet = IsCombinedBet;
                dbBet.Stake = Stake;
                dbBet.Quota = Quota;
                dbBet.Profits = Profits;
                dbBet.Benefits = Benefits;
                dbBet.IdRelUserBookmaker = IdRelUserBookmaker;
                dbBet.IdStatusType = IdStatusType;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current bet.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbBet = dbContext.Bet.Find(IdBet);

                if (dbBet == null)
                    throw new NotFoundBetException(IdBet.ToString());

                PickEntity.Load(this).ForEach(x => x.Delete());

                dbContext.Bet.Remove(dbBet);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current bet as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Bet[ {0} ]", IdBet);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current bet exists in the database.
        /// </summary>
        /// <returns></returns>
        //internal bool Exist()
        //{
        //    using (var dbContext = new BD_TRACKMYBETSContext())
        //    {
        //        return dbContext.Bet.Any(x => x.IdBet == IdBet);
        //    }
        //}

        /// <summary>
        /// Method that maps a bet to the database model.
        /// </summary>
        /// <returns></returns>
        internal Bet MapToBD()
        {
            var dbBet = new Bet
            {
                DateBet = DateBet,
                IsLiveBet = IsLiveBet,
                IsCombinedBet = IsCombinedBet,
                Stake = Stake,
                Quota = Quota,
                Profits = Profits,
                Benefits = Benefits,
                IdRelUserBookmaker = IdRelUserBookmaker,
                IdStatusType = IdStatusType
            };

            return dbBet;
        }

        /// <summary>
        /// Method that maps a bet from a database model passed by parameter.
        /// </summary>
        /// <param name="dbBet"></param>
        /// <returns></returns>
        internal static BetEntity MapFromBD(Bet dbBet)
        {
            var betEntity = new BetEntity()
            {
                IdBet = dbBet.IdBet,
                DateBet = dbBet.DateBet,
                IsLiveBet = dbBet.IsLiveBet,
                IsCombinedBet = dbBet.IsCombinedBet,
                Stake = dbBet.Stake,
                Quota = dbBet.Quota,
                Profits = dbBet.Profits,
                Benefits = dbBet.Benefits,
                IdRelUserBookmaker = dbBet.IdRelUserBookmaker,
                IdStatusType = dbBet.IdStatusType
            };

            return betEntity;
        }
        #endregion
    }
}
