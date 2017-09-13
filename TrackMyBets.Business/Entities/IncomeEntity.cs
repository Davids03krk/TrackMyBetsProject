using System;
using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class IncomeEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdIncome { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime DateIncome { get; set; }
        public bool? IsFreeBonus { get; set; }
        public int IdRelUserBookmaker { get; set; }
        #endregion

        #region Constructor
        public IncomeEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the income.
        /// </summary>
        /// <returns></returns>
        public static List<IncomeEntity> Load()
        {
            var incomes = new List<IncomeEntity>();

            _dbContext.Income.ToList().ForEach(x => incomes.Add(IncomeEntity.Load(x.IdIncome)));

            return incomes;
        }

        /// <summary>
        /// Method that returns the income with the Id passed as parameter. 
        /// </summary>
        /// <param name="incomeId"></param>
        /// <returns></returns>
        public static IncomeEntity Load(int incomeId)
        {
            var dbIncome = _dbContext.Income.Find(incomeId);

            if (dbIncome == null)
                return null;

            return MapFromBD(dbIncome);
        }

        /// <summary>
        /// Method that returns a list of incomes of a rel_user_bookmaker passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<IncomeEntity> Load(RelUserBookmakerEntity relUserBookmaker)
        {
            var incomes = new List<IncomeEntity>();

            _dbContext.Income.ToList().ForEach(x =>
            {
                if (x.IdRelUserBookmaker == relUserBookmaker.IdRelUserBookmaker)
                    incomes.Add(IncomeEntity.Load(x.IdIncome));
            });

            return incomes;
        }

        /// <summary>
        /// Method that adds to the database the past income as a parameter.
        /// </summary>
        /// <param name="income"></param>
        public static void Create(IncomeEntity income)
        {
            var dbIncome = income.MapToBD();

            _dbContext.Income.Add(dbIncome);
            _dbContext.SaveChanges();

            income.IdIncome = dbIncome.IdIncome;
        }

        /// <summary>
        /// Method that updates the current income database.
        /// </summary>
        public void Update()
        {
            var dbIncome = _dbContext.Income.Find(IdIncome);

            if (dbIncome == null)
                throw new NotFoundIncomeException(IdIncome.ToString());

            dbIncome.Amount = Amount;
            dbIncome.Comment = Comment;
            dbIncome.DateIncome = DateIncome;
            dbIncome.IsFreeBonus = IsFreeBonus;
            dbIncome.IdRelUserBookmaker = IdRelUserBookmaker;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current income.
        /// </summary>
        public void Delete()
        {
            var dbIncome = _dbContext.Income.Find(IdIncome);

            if (dbIncome == null)
                throw new NotFoundIncomeException(IdIncome.ToString());
            
            _dbContext.Income.Remove(dbIncome);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that returns the current income as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Income[ {0} ]", IdIncome);
        }
        #endregion

        #region Internal Methods        
        ///// <summary>
        ///// Method that returns if the current income exists in the database.
        ///// </summary>
        ///// <returns></returns>
        //internal bool Exist()
        //{
        //    return _dbContext.Income.Any(x => x.IdIncome == IdIncome);
        //}

        /// <summary>
        /// Method that maps a income to the database model.
        /// </summary>
        /// <returns></returns>
        internal Income MapToBD()
        {
            var dbIncome = new Income
            {
                Amount = Amount,
                Comment = Comment,
                DateIncome = DateIncome,
                IsFreeBonus = IsFreeBonus,
                IdRelUserBookmaker = IdRelUserBookmaker
            };

            return dbIncome;
        }

        /// <summary>
        /// Method that maps a income from a database model passed by parameter.
        /// </summary>
        /// <param name="dbIncome"></param>
        /// <returns></returns>
        internal static IncomeEntity MapFromBD(Income dbIncome)
        {
            var incomeEntity = new IncomeEntity(_dbContext)
            {
                IdIncome = dbIncome.IdIncome,
                Amount = dbIncome.Amount,
                Comment = dbIncome.Comment,
                DateIncome = dbIncome.DateIncome,
                IsFreeBonus = dbIncome.IsFreeBonus,
                IdRelUserBookmaker = dbIncome.IdRelUserBookmaker
            };

            return incomeEntity;
        }
        #endregion
    }
}
