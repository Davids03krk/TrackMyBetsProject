using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Event = new HashSet<Event>();
            Team = new HashSet<Team>();
        }

        public int IdSport { get; set; }
        public string DescSport { get; set; }
        public float? DurationMatchInHours { get; set; }

        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Team> Team { get; set; }
    }
}
