using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class EventModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public EventModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public int IdEvent { get; set; }

        public string Comment { get; set; }

        public DateTime? DateEvent { get; set; }

        [Required]
        public TeamModel LocalTeam { get; set; }

        [Required]
        public TeamModel VisitTeam { get; set; }

        [Required]
        public SportModel Sport { get; set; }
        #endregion

        #region Public Methods
        public static EventModel FromEntity(EventEntity evento)
        {
            EventModel eventModel = new EventModel(_dbContext)
            {
                IdEvent = evento.IdEvent,
                Comment = evento.Comment,
                DateEvent = evento.DateEvent,
                LocalTeam = TeamModel.FromEntity(TeamEntity.Load(evento.IdLocalTeam)),
                VisitTeam = TeamModel.FromEntity(TeamEntity.Load(evento.IdVisitTeam)),
                Sport = SportModel.FromEntity(SportEntity.Load(evento.IdSport))
            };

            return eventModel;
        }

        public EventEntity ToEntities()
        {
            var eventEntity = new EventEntity(_dbContext);

            eventEntity.IdEvent = this.IdEvent;
            eventEntity.Comment = this.Comment;
            eventEntity.DateEvent = this.DateEvent;
            eventEntity.IdLocalTeam = this.LocalTeam.IdTeam;
            eventEntity.IdVisitTeam = this.VisitTeam.IdTeam;
            eventEntity.IdSport = this.Sport.IdSport;

            return eventEntity;
        }
        #endregion
    }
}
