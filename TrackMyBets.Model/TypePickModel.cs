using System;
using System.Collections.Generic;
using System.Text;
using TrackMyBets.Business.Functions;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public abstract class TypePickModel
    {
        #region Properties
        public int IdPick { get; set; }

        public Enumerators.PickTypes TypePick { get; set; }
        #endregion
    }
}

