using TrackMyBets.Business.Functions;

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

