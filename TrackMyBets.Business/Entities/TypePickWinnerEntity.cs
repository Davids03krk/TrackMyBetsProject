using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class TypePickWinnerEntity
    {
        #region Attributes
        public int IdPick { get; set; }
        public bool? HasHandicap { get; set; }
        public float? ValueHandicap { get; set; }
        public int IdWinnerTeam { get; set; }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the pick of type winner.
        /// </summary>
        /// <returns></returns>
        public static List<TypePickWinnerEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var typePicksWinner = new List<TypePickWinnerEntity>();

                dbContext.TypePickWinner.ToList().ForEach(x => typePicksWinner.Add(TypePickWinnerEntity.Load(x.IdPick)));

                return typePicksWinner;
            }
        }

        /// <summary>
        /// Method that returns the pick of type winner with the Id passed as parameter. 
        /// </summary>
        /// <param name="pickId"></param>
        /// <returns></returns>
        public static TypePickWinnerEntity Load(int pickId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbTypePickWinner = dbContext.TypePickWinner.Find(pickId);

                if (dbTypePickWinner == null)
                    return null;

                return MapFromBD(dbTypePickWinner);
            }
        }

        /// <summary>
        /// Method that adds to the database the past pick of type winner as a parameter.
        /// </summary>
        /// <param name="typePickWinner"></param>
        public static void Create(TypePickWinnerEntity typePickWinner)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickWinner = typePickWinner.MapToBD();

                dbContext.TypePickWinner.Add(dbtypePickWinner);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that updates the current pick of type winner database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickWinner = dbContext.TypePickWinner.Find(IdPick);

                if (dbtypePickWinner == null)
                    throw new NotFoundPickException(IdPick.ToString());

                dbtypePickWinner.HasHandicap = HasHandicap;
                dbtypePickWinner.ValueHandicap = ValueHandicap;
                dbtypePickWinner.IdWinnerTeam = IdWinnerTeam;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current pick of type winner.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickWinner = dbContext.TypePickWinner.Find(IdPick);

                if (dbtypePickWinner == null)
                    throw new NotFoundPickException(IdPick.ToString());

                dbContext.TypePickWinner.Remove(dbtypePickWinner);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current pick as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Pick[ {0}, Type: Winner ]", IdPick);
        }
        #endregion


        #region Internal Methods        
        ///// <summary>
        ///// Method that returns if the current pick exists in the database.
        ///// </summary>
        ///// <returns></returns>
        internal bool Exist()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                return dbContext.TypePickTotalPoints.Any(x => x.IdPick == IdPick)
                || dbContext.TypePickWinner.Any(x => x.IdPick == IdPick);
            }
        }

        /// <summary>
        /// Method that maps a pick of type winner to the database model.
        /// </summary>
        /// <returns></returns>
        internal TypePickWinner MapToBD()
        {
            var dbTypePickWinner = new TypePickWinner
            {
                IdPick = IdPick,
                HasHandicap = HasHandicap,
                ValueHandicap = ValueHandicap,
                IdWinnerTeam = IdWinnerTeam
            };

            return dbTypePickWinner;
        }

        /// <summary>
        /// Method that maps a pick of type winner from a database model passed by parameter.
        /// </summary>
        /// <param name="dbTypePickWinner"></param>
        /// <returns></returns>
        internal static TypePickWinnerEntity MapFromBD(TypePickWinner dbTypePickWinner)
        {
            var typePickWinnerEntity = new TypePickWinnerEntity()
            {
                IdPick = dbTypePickWinner.IdPick,
                HasHandicap = dbTypePickWinner.HasHandicap,
                ValueHandicap = dbTypePickWinner.ValueHandicap,
                IdWinnerTeam = dbTypePickWinner.IdWinnerTeam
            };

            return typePickWinnerEntity;
        }
        #endregion
    }
}
