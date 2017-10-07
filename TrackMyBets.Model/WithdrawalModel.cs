using System;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class WithdrawalModel
    {
        #region Properties
        public int IdWithdrawal { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime DateWithdrawal { get; set; }

        [Required]
        public int RelUserBookmaker { get; set; }

        [Required]
        public UserModel User { get; set; }

        [Required]
        public BookmakerModel Bookmaker { get; set; }
        #endregion

        #region Public Methods
        public static WithdrawalModel FromEntity(WithdrawalEntity withdrawal)
        {
            RelUserBookmakerEntity relUserBookmakerEntity = RelUserBookmakerEntity.Load(withdrawal.IdRelUserBookmaker);

            WithdrawalModel withdrawalModel = new WithdrawalModel()
            {
                IdWithdrawal = withdrawal.IdWithdrawal,
                Amount = withdrawal.Amount,
                Comment = withdrawal.Comment,
                DateWithdrawal = withdrawal.DateWithdrawal,
                RelUserBookmaker = withdrawal.IdRelUserBookmaker,
                User = UserModel.FromEntity(UserEntity.Load(relUserBookmakerEntity.IdUser)),
                Bookmaker = BookmakerModel.FromEntity(BookmakerEntity.Load(relUserBookmakerEntity.IdBookmaker))
            };

            return withdrawalModel;
        }

        public WithdrawalEntity ToEntities()
        {
            var withdrawalEntity = new WithdrawalEntity()
            {
                IdWithdrawal = IdWithdrawal,
                Amount = Amount,
                Comment = Comment,
                DateWithdrawal = DateWithdrawal,
                IdRelUserBookmaker = RelUserBookmaker
            };

            return withdrawalEntity;
        }
        #endregion
    }
}
