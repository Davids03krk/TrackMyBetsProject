using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Event
    {
        public Event()
        {
            Pick = new HashSet<Pick>();
        }

        public int IdEvent { get; set; }
        public string Comment { get; set; }
        public DateTime? DateEvent { get; set; }
        public int IdLocalTeam { get; set; }
        public int IdVisitTeam { get; set; }
        public int IdSport { get; set; }

        public virtual ICollection<Pick> Pick { get; set; }
        public virtual Team IdLocalTeamNavigation { get; set; }
        public virtual Sport IdSportNavigation { get; set; }
        public virtual Team IdVisitTeamNavigation { get; set; }
    }
}
