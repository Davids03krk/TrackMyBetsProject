using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Bet
    {
        public Bet()
        {
            Pick = new HashSet<Pick>();
        }

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

        public virtual ICollection<Pick> Pick { get; set; }
        public virtual RelUserBookmaker IdRelUserBookmakerNavigation { get; set; }
        public virtual StatusType IdStatusTypeNavigation { get; set; }
    }
}
