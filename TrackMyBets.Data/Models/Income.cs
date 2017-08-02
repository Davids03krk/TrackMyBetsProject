using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Income
    {
        public int IdIncome { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime DateIncome { get; set; }
        public bool? IsFreeBonus { get; set; }
        public int IdRelUserBookmaker { get; set; }

        public virtual RelUserBookmaker IdRelUserBookmakerNavigation { get; set; }
    }
}
