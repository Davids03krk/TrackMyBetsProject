using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrackMyBets.Business.Functions
{
    /// <summary>
    /// Class to store all enumerators for functions
    /// </summary>
    public static class Enumerators
    {
        /// <summary>
        /// Type of Pick
        /// </summary>
        public enum PickTypes
        {
            [Display(Name = "WINNER", Description = "Winner")]
            WINNER = 1,
            [Display(Name = "TOTAL_POINTS", Description = "Total Points")]
            TOTAL_POINTS = 2
        }
    }
}
