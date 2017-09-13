using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class TeamEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdTeam { get; set; }
        public string DescTeam { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Stadium { get; set; }
        public string Abbreviation { get; set; }
        public int IdSport { get; set; }
        #endregion

        #region Constructor
        public TeamEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Method that returns a list with all the team.
        /// </summary>
        /// <returns></returns>
        public static List<TeamEntity> Load()
        {
            var teams = new List<TeamEntity>();

            _dbContext.Team.ToList().ForEach(x => teams.Add(TeamEntity.Load(x.IdTeam)));

            return teams;
        }

        /// <summary>
        /// Method that returns the team with the Id passed as parameter. 
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public static TeamEntity Load(int teamId)
        {
            var dbTeam = _dbContext.Team.Find(teamId);

            if (dbTeam == null)
                return null;

            return MapFromBD(dbTeam);
        }

        /// <summary>
        /// Method that returns a list of teams that belong to a sport passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<TeamEntity> Load(SportEntity sport)
        {
            var teams = new List<TeamEntity>();

            _dbContext.Team.ToList().ForEach(x =>
            {
                if (x.IdSport == sport.IdSport)
                    teams.Add(TeamEntity.Load(x.IdTeam));
            });

            return teams;
        }

        /// <summary>
        /// Method that adds to the database the past team as a parameter.
        /// </summary>
        /// <param name="team"></param>
        public static void Create(TeamEntity team)
        {
            if (team.Exist())
                throw new DuplicatedTeamException(team.ToString());

            var dbTeam = team.MapToBD();
            _dbContext.Team.Add(dbTeam);
            _dbContext.SaveChanges();

            team.IdTeam = dbTeam.IdTeam;
        }

        /// <summary>
        /// Method that updates the current team database.
        /// </summary>
        public void Update()
        {
            var dbTeam = _dbContext.Team.Find(IdTeam);

            if (dbTeam == null)
                throw new NotFoundTeamException(IdTeam.ToString());

            dbTeam.DescTeam = DescTeam;
            dbTeam.Name = Name;
            dbTeam.City = City;
            dbTeam.Stadium = Stadium;
            dbTeam.IdSport = IdSport;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current team.
        /// </summary>
        public void Delete()
        {
            var dbTeam = _dbContext.Team.Find(IdTeam);

            if (dbTeam == null)
                throw new NotFoundTeamException(IdTeam.ToString());

            EventEntity.Load(this).ForEach(x => x.Delete());

            _dbContext.Team.Remove(dbTeam);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that returns the current team as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Team[ {0} ]", DescTeam);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current team exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist()
        {
            return _dbContext.Team.Any(x => x.DescTeam == DescTeam);
        }

        /// <summary>
        /// Method that maps a team to the database model.
        /// </summary>
        /// <returns></returns>
        internal Team MapToBD()
        {
            var dbTeam = new Team
            {
                DescTeam = DescTeam,
                Name = Name,
                City = City,
                Stadium = Stadium,
                IdSport = IdSport
            };

            return dbTeam;
        }

        /// <summary>
        /// Method that maps a team from a database model passed by parameter.
        /// </summary>
        /// <param name="dbTeam"></param>
        /// <returns></returns>
        internal static TeamEntity MapFromBD(Team dbTeam)
        {
            var teamEntity = new TeamEntity(_dbContext)
            {
                IdTeam = dbTeam.IdTeam,
                DescTeam = dbTeam.DescTeam,
                Name = dbTeam.Name,
                City = dbTeam.City,
                Stadium = dbTeam.Stadium,
                IdSport = dbTeam.IdSport
            };

            return teamEntity;
        }
        #endregion
    }
}
