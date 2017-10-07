using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class PickEntity
    {
        #region Attributes
        public int IdPick { get; set; }
        public int IdBet { get; set; }
        public int IdEvent { get; set; }
        public int IdPickType { get; set; }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the pick.
        /// </summary>
        /// <returns></returns>
        public static List<PickEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var picks = new List<PickEntity>();

                dbContext.Pick.ToList().ForEach(x => picks.Add(PickEntity.Load(x.IdPick)));

                return picks;
            }
        }

        /// <summary>
        /// Method that returns the pick with the Id passed as parameter. 
        /// </summary>
        /// <param name="pickId"></param>
        /// <returns></returns>
        public static PickEntity Load(int pickId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbPick = dbContext.Pick.Find(pickId);

                if (dbPick == null)
                    return null;

                return MapFromBD(dbPick);
            }
        }

        /// <summary>
        /// Method that returns a list of picks of a sports event passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<PickEntity> Load(EventEntity evento)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var picks = new List<PickEntity>();

                dbContext.Pick.ToList().ForEach(x =>
                {
                    if (x.IdEvent == evento.IdEvent)
                        picks.Add(PickEntity.Load(x.IdPick));
                });

                return picks;
            }
        }

        /// <summary>
        /// Method that returns a list of picks of a bet passed as a parameter.
        /// </summary>
        /// <returns></returns>
        public static List<PickEntity> Load(BetEntity bet)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var picks = new List<PickEntity>();

                dbContext.Pick.ToList().ForEach(x =>
                {
                    if (x.IdBet == bet.IdBet)
                        picks.Add(PickEntity.Load(x.IdPick));
                });

                return picks;
            }
        }

        /// <summary>
        /// Method that adds to the database the past pick as a parameter.
        /// </summary>
        /// <param name="pick"></param>
        public static void Create(PickEntity pick)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbPick = pick.MapToBD();

                dbContext.Pick.Add(dbPick);
                dbContext.SaveChanges();

                pick.IdPick = dbPick.IdPick;
            }
        }

        /// <summary>
        /// Method that updates the current pick database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbPick = dbContext.Pick.Find(IdPick);

                if (dbPick == null)
                    throw new NotFoundPickException(IdPick.ToString());

                dbPick.IdBet = IdBet;
                dbPick.IdEvent = IdEvent;
                dbPick.IdPickType = IdPickType;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current pick.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbPick = dbContext.Pick.Find(IdPick);

                if (dbPick == null)
                    throw new NotFoundPickException(IdPick.ToString());

                TypePickTotalPointsEntity.Load(IdPick).Delete();
                TypePickWinnerEntity.Load(IdPick).Delete();

                dbContext.Pick.Remove(dbPick);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current pick as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Pick[ {0} ]", IdPick);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current pick exists in the database.
        /// </summary>
        /// <returns></returns>
        //internal bool Exist()
        //{
        //    using (var dbContext = new BD_TRACKMYBETSContext())
        //    {
        //        return dbContext.Pick.Any(x => x.IdPick == IdPick);
        //    }
        //}

        /// <summary>
        /// Method that maps a pick to the database model.
        /// </summary>
        /// <returns></returns>
        internal Pick MapToBD()
        {
            var dbPick = new Pick
            {
                IdBet = IdBet,
                IdEvent = IdEvent,
                IdPickType = IdPickType
            };

            return dbPick;
        }

        /// <summary>
        /// Method that maps a pick from a database model passed by parameter.
        /// </summary>
        /// <param name="dbPick"></param>
        /// <returns></returns>
        internal static PickEntity MapFromBD(Pick dbPick)
        {
            var pickEntity = new PickEntity()
            {
                IdPick = dbPick.IdPick,
                IdBet = dbPick.IdBet,
                IdEvent = dbPick.IdEvent,
                IdPickType = dbPick.IdPickType
            };

            return pickEntity;
        }
        #endregion
    }
}