﻿using System;
using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class EventEntity
    {
        #region Attributes
        public int IdEvent { get; set; }
        public string Comment { get; set; }
        public DateTime? DateEvent { get; set; }
        public int IdLocalTeam { get; set; }
        public int IdVisitTeam { get; set; }
        public int IdSport { get; set; }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the event.
        /// </summary>
        /// <returns></returns>
        public static List<EventEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var events = new List<EventEntity>();

                dbContext.Event.ToList().ForEach(x => events.Add(EventEntity.Load(x.IdEvent)));

                return events;
            }
        }

        /// <summary>
        /// Method that returns the event with the Id passed as parameter. 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public static EventEntity Load(int eventId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbEvent = dbContext.Event.Find(eventId);

                if (dbEvent == null)
                    return null;

                return MapFromBD(dbEvent);
            }
        }

        /// <summary>
        /// Method that returns a list of events in which a team passed as a parameter participates.
        /// </summary>
        /// <returns></returns>
        public static List<EventEntity> Load(TeamEntity team)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var events = new List<EventEntity>();

                dbContext.Event.ToList().ForEach(x =>
                {
                    if (x.IdLocalTeam == team.IdTeam || x.IdVisitTeam == team.IdTeam)
                        events.Add(EventEntity.Load(x.IdEvent));
                });

                return events;
            }
        }

        /// <summary>
        /// Method that returns a list of events of a sport passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<EventEntity> Load(SportEntity sport)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var events = new List<EventEntity>();

                dbContext.Event.ToList().ForEach(x =>
                {
                    if (x.IdSport == sport.IdSport)
                        events.Add(EventEntity.Load(x.IdEvent));
                });

                return events;
            }
        }

        /// <summary>
        /// Method that adds to the database the past event as a parameter.
        /// </summary>
        /// <param name="evento"></param>
        public static void Create(EventEntity evento)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                if (evento.Exist())
                    throw new DuplicatedEventException(evento.ToString());

                var dbEvent = evento.MapToBD();
                dbContext.Event.Add(dbEvent);
                dbContext.SaveChanges();

                evento.IdEvent = dbEvent.IdEvent;
            }
        }

        /// <summary>
        /// Method that updates the current event database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbEvent = dbContext.Event.Find(IdEvent);

                if (dbEvent == null)
                    throw new NotFoundEventException(IdEvent.ToString());

                dbEvent.Comment = Comment;
                dbEvent.DateEvent = DateEvent;
                dbEvent.IdLocalTeam = IdLocalTeam;
                dbEvent.IdVisitTeam = IdVisitTeam;
                dbEvent.IdSport = IdSport;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current event.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbEvent = dbContext.Event.Find(IdEvent);

                if (dbEvent == null)
                    throw new NotFoundEventException(IdEvent.ToString());

                PickEntity.Load(this).ForEach(x => x.Delete());

                dbContext.Event.Remove(dbEvent);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current event as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Event[ {0} vs {1}, {2} ]", IdVisitTeam, IdLocalTeam, DateEvent);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current event exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                return dbContext.Event.Any(x => x.IdLocalTeam == IdLocalTeam && x.IdVisitTeam == IdVisitTeam && x.DateEvent == DateEvent);
            }
        }

        /// <summary>
        /// Method that maps a event to the database model.
        /// </summary>
        /// <returns></returns>
        internal Event MapToBD()
        {
            var dbEvent = new Event
            {
                Comment = Comment,
                DateEvent = DateEvent,
                IdLocalTeam = IdLocalTeam,
                IdVisitTeam = IdVisitTeam,
                IdSport = IdSport
            };

            return dbEvent;
        }

        /// <summary>
        /// Method that maps a event from a database model passed by parameter.
        /// </summary>
        /// <param name="dbEvent"></param>
        /// <returns></returns>
        internal static EventEntity MapFromBD(Event dbEvent)
        {
            var eventEntity = new EventEntity()
            {
                IdEvent = dbEvent.IdEvent,
                Comment = dbEvent.Comment,
                DateEvent = dbEvent.DateEvent,
                IdLocalTeam = dbEvent.IdLocalTeam,
                IdVisitTeam = dbEvent.IdVisitTeam,
                IdSport = dbEvent.IdSport
            };

            return eventEntity;
        }
        #endregion
    }
}
