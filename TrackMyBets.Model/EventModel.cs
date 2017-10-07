using System;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class EventModel
    {
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
            EventModel eventModel = new EventModel()
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
            var eventEntity = new EventEntity()
            {
                IdEvent = IdEvent,
                Comment = Comment,
                DateEvent = DateEvent,
                IdLocalTeam = LocalTeam.IdTeam,
                IdVisitTeam = VisitTeam.IdTeam,
                IdSport = Sport.IdSport
            };

            return eventEntity;
        }
        #endregion
    }
}
