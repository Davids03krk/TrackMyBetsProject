namespace TrackMyBets.WepApi.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public static string ConnetionString { get; set; }

        public AppSettings() {
            ConnetionString = "Data Source=DESKTOP-IKARG49;Initial Catalog=BD_TRACKMYBETS;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}

