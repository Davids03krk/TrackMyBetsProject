using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Team
    {
        public Team()
        {
            EventIdLocalTeamNavigation = new HashSet<Event>();
            EventIdVisitTeamNavigation = new HashSet<Event>();
        }

        public int IdTeam { get; set; }
        public string DescTeam { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Stadium { get; set; }
        public string Abbreviation { get; set; }
        public int IdSport { get; set; }

        public virtual ICollection<Event> EventIdLocalTeamNavigation { get; set; }
        public virtual ICollection<Event> EventIdVisitTeamNavigation { get; set; }
        public virtual Sport IdSportNavigation { get; set; }
    }
}
