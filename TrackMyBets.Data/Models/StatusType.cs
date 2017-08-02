using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class StatusType
    {
        public StatusType()
        {
            Bet = new HashSet<Bet>();
        }

        public int IdStatusType { get; set; }
        public string DescStatusType { get; set; }

        public virtual ICollection<Bet> Bet { get; set; }
    }
}
