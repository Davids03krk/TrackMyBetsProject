using System;
using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class WithdrawalEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdWithdrawal { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime DateWithdrawal { get; set; }
        public int IdRelUserBookmaker { get; set; }
        #endregion

        #region Constructor
        public WithdrawalEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the withdrawals.
        /// </summary>
        /// <returns></returns>
        public static List<WithdrawalEntity> Load()
        {
            var withdrawals = new List<WithdrawalEntity>();

            _dbContext.Withdrawal.ToList().ForEach(x => withdrawals.Add(WithdrawalEntity.Load(x.IdWithdrawal)));

            return withdrawals;
        }

        /// <summary>
        /// Method that returns the withdrawal with the Id passed as parameter. 
        /// </summary>
        /// <param name="withdrawalId"></param>
        /// <returns></returns>
        public static WithdrawalEntity Load(int withdrawalId)
        {
            var dbWithdrawal = _dbContext.Withdrawal.Find(withdrawalId);

            if (dbWithdrawal == null)
                return null;

            return MapFromBD(dbWithdrawal);
        }

        /// <summary>
        /// Method that returns a list of withdrawals of a rel_user_bookmaker passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<WithdrawalEntity> Load(RelUserBookmakerEntity relUserBookmaker)
        {
            var withdrawals = new List<WithdrawalEntity>();

            _dbContext.Withdrawal.ToList().ForEach(x =>
            {
                if (x.IdRelUserBookmaker == relUserBookmaker.IdRelUserBookmaker)
                    withdrawals.Add(WithdrawalEntity.Load(x.IdWithdrawal));
            });

            return withdrawals;
        }

        /// <summary>
        /// Method that adds to the database the past withdrawal as a parameter.
        /// </summary>
        /// <param name="withdrawal"></param>
        public static void Create(WithdrawalEntity withdrawal)
        {
            var dbWithdrawal = withdrawal.MapToBD();

            _dbContext.Withdrawal.Add(dbWithdrawal);
            _dbContext.SaveChanges();

            withdrawal.IdWithdrawal = dbWithdrawal.IdWithdrawal;
        }

        /// <summary>
        /// Method that updates the current withdrawal database.
        /// </summary>
        public void Update()
        {
            var dbWithdrawal = _dbContext.Withdrawal.Find(IdWithdrawal);

            if (dbWithdrawal == null)
                throw new NotFoundWithdrawalException(IdWithdrawal.ToString());

            dbWithdrawal.Amount = Amount;
            dbWithdrawal.Comment = Comment;
            dbWithdrawal.DateWithdrawal = DateWithdrawal;
            dbWithdrawal.IdRelUserBookmaker = IdRelUserBookmaker;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current withdrawal.
        /// </summary>
        public void Delete()
        {
            var dbWithdrawal = _dbContext.Withdrawal.Find(IdWithdrawal);

            if (dbWithdrawal == null)
                throw new NotFoundWithdrawalException(IdWithdrawal.ToString());

            _dbContext.Withdrawal.Remove(dbWithdrawal);
            _dbContext.SaveChanges();
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
        //    return _dbContext.Withdrawal.Any(x => x.IdWithdrawal == IdWithdrawal);
        //}

        /// <summary>
        /// Method that maps a withdrawal to the database model.
        /// </summary>
        /// <returns></returns>
        internal Withdrawal MapToBD()
        {
            var dbWithdrawal = new Withdrawal();

            dbWithdrawal.Amount = Amount;
            dbWithdrawal.Comment = Comment;
            dbWithdrawal.DateWithdrawal = DateWithdrawal;
            dbWithdrawal.IdRelUserBookmaker = IdRelUserBookmaker;

            return dbWithdrawal;
        }

        /// <summary>
        /// Method that maps a withdrawal from a database model passed by parameter.
        /// </summary>
        /// <param name="dbWithdrawal"></param>
        /// <returns></returns>
        internal static WithdrawalEntity MapFromBD(Withdrawal dbWithdrawal)
        {
            var withdrawalEntity = new WithdrawalEntity(_dbContext);

            withdrawalEntity.IdWithdrawal = dbWithdrawal.IdWithdrawal;
            withdrawalEntity.Amount = dbWithdrawal.Amount;
            withdrawalEntity.Comment = dbWithdrawal.Comment;
            withdrawalEntity.DateWithdrawal = dbWithdrawal.DateWithdrawal;
            withdrawalEntity.IdRelUserBookmaker = dbWithdrawal.IdRelUserBookmaker;

            return withdrawalEntity;
        }
        #endregion
    }
}