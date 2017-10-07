namespace TrackMyBets.Data.Configurations
{
    public class Settings
    {
        public static string ConnectionString { get; set; }

        public Settings() {
            ConnectionString = "Data Source=DESKTOP-IKARG49;Initial Catalog=BD_TRACKMYBETS;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}

