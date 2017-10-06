using System.Collections.Generic;

namespace TrackMyBets.Data.Models
{
    public partial class User
    {
        public User()
        {
            RelUserBookmaker = new HashSet<RelUserBookmaker>();
        }

        public int IdUser { get; set; }
        public string Nick { get; set; }
        public string Name { get; set; }
        public string SurnameFirst { get; set; }
        public string SurnameSecond { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<RelUserBookmaker> RelUserBookmaker { get; set; }
    }
}
