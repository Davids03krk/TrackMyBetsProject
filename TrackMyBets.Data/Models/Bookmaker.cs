using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class Bookmaker
    {
        public Bookmaker()
        {
            RelUserBookmaker = new HashSet<RelUserBookmaker>();
        }

        public int IdBookmaker { get; set; }
        public string DescBookmaker { get; set; }

        public virtual ICollection<RelUserBookmaker> RelUserBookmaker { get; set; }
    }
}
