using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class TypePickTotalPoints
    {
        public int IdPick { get; set; }
        public bool? IsOver { get; set; }
        public bool? IsUnder { get; set; }
        public float ValueTotalPoints { get; set; }
    }
}
