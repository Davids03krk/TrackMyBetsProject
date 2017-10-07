using System;
using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class WithdrawalEntity
    {
        #region Attributes
        public int IdWithdrawal { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime DateWithdrawal { get; set; }
        public int IdRelUserBookmaker { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the withdrawals.
        /// </summary>
        /// <returns></returns>
        public static List<WithdrawalEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var withdrawals = new List<WithdrawalEntity>();

                dbContext.Withdrawal.ToList().ForEach(x => withdrawals.Add(WithdrawalEntity.Load(x.IdWithdrawal)));

                return withdrawals;
            }
        }

        /// <summary>
        /// Method that returns the withdrawal with the Id passed as parameter. 
        /// </summary>
        /// <param name="withdrawalId"></param>
        /// <returns></returns>
        public static WithdrawalEntity Load(int withdrawalId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbWithdrawal = dbContext.Withdrawal.Find(withdrawalId);

                if (dbWithdrawal == null)
                    return null;

                return MapFromBD(dbWithdrawal);
            }
        }

        /// <summary>
        /// Method that returns a list of withdrawals of a rel_user_bookmaker passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<WithdrawalEntity> Load(RelUserBookmakerEntity relUserBookmaker)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var withdrawals = new List<WithdrawalEntity>();

                dbContext.Withdrawal.ToList().ForEach(x =>
                {
                    if (x.IdRelUserBookmaker == relUserBookmaker.IdRelUserBookmaker)
                        withdrawals.Add(WithdrawalEntity.Load(x.IdWithdrawal));
                });

                return withdrawals;
            }
        }

        /// <summary>
        /// Method that adds to the database the past withdrawal as a parameter.
        /// </summary>
        /// <param name="withdrawal"></param>
        public static void Create(WithdrawalEntity withdrawal)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbWithdrawal = withdrawal.MapToBD();

                dbContext.Withdrawal.Add(dbWithdrawal);
                dbContext.SaveChanges();

                withdrawal.IdWithdrawal = dbWithdrawal.IdWithdrawal;
            }
        }

        /// <summary>
        /// Method that updates the current withdrawal database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbWithdrawal = dbContext.Withdrawal.Find(IdWithdrawal);

                if (dbWithdrawal == null)
                    throw new NotFoundWithdrawalException(IdWithdrawal.ToString());

                dbWithdrawal.Amount = Amount;
                dbWithdrawal.Comment = Comment;
                dbWithdrawal.DateWithdrawal = DateWithdrawal;
                dbWithdrawal.IdRelUserBookmaker = IdRelUserBookmaker;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current withdrawal.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbWithdrawal = dbContext.Withdrawal.Find(IdWithdrawal);

                if (dbWithdrawal == null)
                    throw new NotFoundWithdrawalException(IdWithdrawal.ToString());

                dbContext.Withdrawal.Remove(dbWithdrawal);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current withdrawal as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Withdrawal[ {0} ]", IdWithdrawal);
        }
        #endregion

        #region Internal Methods        
        ///// <summary>
        ///// Method that returns if the current withdrawal exists in the database.
        ///// </summary>
        ///// <returns></returns>
        //internal bool Exist()
        //{
        //    using (var dbContext = new BD_TRACKMYBETSContext())
        //    {
        //        return _dbContext.Withdrawal.Any(x => x.IdWithdrawal == IdWithdrawal);
        //    }
        //}

        /// <summary>
        /// Method that maps a withdrawal to the database model.
        /// </summary>
        /// <returns></returns>
        internal Withdrawal MapToBD()
        {
            var dbWithdrawal = new Withdrawal
            {
                Amount = Amount,
                Comment = Comment,
                DateWithdrawal = DateWithdrawal,
                IdRelUserBookmaker = IdRelUserBookmaker
            };

            return dbWithdrawal;
        }

        /// <summary>
        /// Method that maps a withdrawal from a database model passed by parameter.
        /// </summary>
        /// <param name="dbWithdrawal"></param>
        /// <returns></returns>
        internal static WithdrawalEntity MapFromBD(Withdrawal dbWithdrawal)
        {
            var withdrawalEntity = new WithdrawalEntity()
            {
                IdWithdrawal = dbWithdrawal.IdWithdrawal,
                Amount = dbWithdrawal.Amount,
                Comment = dbWithdrawal.Comment,
                DateWithdrawal = dbWithdrawal.DateWithdrawal,
                IdRelUserBookmaker = dbWithdrawal.IdRelUserBookmaker
            };

            return withdrawalEntity;
        }
        #endregion
    }
}