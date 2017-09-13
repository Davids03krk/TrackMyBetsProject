namespace TrackMyBets.Data.Models
{
    public partial class Pick
    {
        public int IdPick { get; set; }
        public int IdBet { get; set; }
        public int IdEvent { get; set; }
        public int IdPickType { get; set; }

        public virtual Bet IdBetNavigation { get; set; }
        public virtual Event IdEventNavigation { get; set; }
        public virtual PickType IdPickTypeNavigation { get; set; }
    }
}
