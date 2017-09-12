using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class TeamModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public TeamModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

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
            TeamModel teamModel = new TeamModel(_dbContext)
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
            var teamEntity = new TeamEntity(_dbContext);

            teamEntity.IdTeam = this.IdTeam;
            teamEntity.DescTeam = this.DescTeam;
            teamEntity.Name = this.Name;
            teamEntity.City = this.City;
            teamEntity.Stadium = this.Stadium;
            teamEntity.Abbreviation = this.Abbreviation;
            teamEntity.IdSport = this.Sport.IdSport;

            return teamEntity;
        }
        #endregion
    }
}
