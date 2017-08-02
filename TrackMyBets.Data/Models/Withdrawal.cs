using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Withdrawal
    {
        public int IdWithdrawal { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime DateWithdrawal { get; set; }
        public int IdRelUserBookmaker { get; set; }

        public virtual RelUserBookmaker IdRelUserBookmakerNavigation { get; set; }
    }
}
