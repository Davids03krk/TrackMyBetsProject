using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class TeamModel
    {
        #region Properties
        public int IdTeam { get; set; }

        public string DescTeam { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        public string Stadium { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public SportModel Sport { get; set; }
        #endregion

        #region Public Methods
        public static TeamModel FromEntity(TeamEntity team)
        {
            TeamModel teamModel = new TeamModel()
            {
                IdTeam = team.IdTeam,
                DescTeam = team.DescTeam,
                Name = team.Name,
                City = team.City,
                Stadium = team.Stadium,
                Abbreviation = team.Abbreviation,
                Sport = SportModel.FromEntity(SportEntity.Load(team.IdSport))
            };

            return teamModel;
        }

        public TeamEntity ToEntities()
        {
            var teamEntity = new TeamEntity()
            {
                IdTeam = IdTeam,
                DescTeam = DescTeam,
                Name = Name,
                City = City,
                Stadium = Stadium,
                Abbreviation = Abbreviation,
                IdSport = Sport.IdSport
            };

            return teamEntity;
        }
        #endregion
    }
}
