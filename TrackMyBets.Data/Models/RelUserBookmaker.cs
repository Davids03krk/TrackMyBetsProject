using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class RelUserBookmaker
    {
        public RelUserBookmaker()
        {
            Bet = new HashSet<Bet>();
            Income = new HashSet<Income>();
            Withdrawal = new HashSet<Withdrawal>();
        }

        public int IdRelUserBookmaker { get; set; }
        public decimal Bankroll { get; set; }
        public int IdUser { get; set; }
        public int IdBookmaker { get; set; }

        public virtual ICollection<Bet> Bet { get; set; }
        public virtual ICollection<Income> Income { get; set; }
        public virtual ICollection<Withdrawal> Withdrawal { get; set; }
        public virtual Bookmaker IdBookmakerNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
