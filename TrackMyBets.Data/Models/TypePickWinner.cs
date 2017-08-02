using System;
using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class TypePickWinner
    {
        public int IdPick { get; set; }
        public bool? HasHandicap { get; set; }
        public float? ValueHandicap { get; set; }
        public int IdWinnerTeam { get; set; }
    }
}
