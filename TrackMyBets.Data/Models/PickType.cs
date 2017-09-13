using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class PickType
    {
        public PickType()
        {
            Pick = new HashSet<Pick>();
        }

        public int IdPickType { get; set; }
        public string DescPickType { get; set; }

        public virtual ICollection<Pick> Pick { get; set; }
    }
}
