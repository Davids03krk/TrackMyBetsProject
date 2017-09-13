using System;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class IncomeModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public IncomeModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdIncome { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime DateIncome { get; set; }

        public bool? IsFreeBonus { get; set; }

        [Required]
        public int RelUserBookmaker { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        public BookmakerModel Bookmaker { get; set; }
        #endregion

        #region Public Methods
        public static IncomeModel FromEntity(IncomeEntity income)
        {
            RelUserBookmakerEntity relUserBookmakerEntity = RelUserBookmakerEntity.Load(income.IdRelUserBookmaker);

            IncomeModel incomeModel = new IncomeModel(_dbContext)
            {
                IdIncome = income.IdIncome,
                Amount = income.Amount,
                Comment = income.Comment,
                DateIncome = income.DateIncome,
                IsFreeBonus = income.IsFreeBonus,
                RelUserBookmaker = income.IdRelUserBookmaker,
                User = UserModel.FromEntity(UserEntity.Load(relUserBookmakerEntity.IdUser)),
                Bookmaker = BookmakerModel.FromEntity(BookmakerEntity.Load(relUserBookmakerEntity.IdBookmaker))
            };

            return incomeModel;
        }

        public IncomeEntity ToEntities()
        {
            var incomeEntity = new IncomeEntity(_dbContext)
            {
                IdIncome = IdIncome,
                Amount = Amount,
                Comment = Comment,
                DateIncome = DateIncome,
                IsFreeBonus = IsFreeBonus,
                IdRelUserBookmaker = RelUserBookmaker
            };

            return incomeEntity;
        }
        #endregion
    }
}
