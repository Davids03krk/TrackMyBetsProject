using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class TypePickTotalPointsEntity
    {
        #region Attributes
        public int IdPick { get; set; }
        public bool? IsOver { get; set; }
        public bool? IsUnder { get; set; }
        public float ValueTotalPoints { get; set; }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the pick of type total points.
        /// </summary>
        /// <returns></returns>
        public static List<TypePickTotalPointsEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var typePicksTotalPoints = new List<TypePickTotalPointsEntity>();

                dbContext.TypePickTotalPoints.ToList().ForEach(x => typePicksTotalPoints.Add(TypePickTotalPointsEntity.Load(x.IdPick)));

                return typePicksTotalPoints;
            }
        }

        /// <summary>
        /// Method that returns the pick of type total points with the Id passed as parameter. 
        /// </summary>
        /// <param name="pickId"></param>
        /// <returns></returns>
        public static TypePickTotalPointsEntity Load(int pickId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickTotalPoints = dbContext.TypePickTotalPoints.Find(pickId);

                if (dbtypePickTotalPoints == null)
                    return null;

                return MapFromBD(dbtypePickTotalPoints);
            }
        }

        /// <summary>
        /// Method that adds to the database the past pick of type total points as a parameter.
        /// </summary>
        /// <param name="typePicksTotalPoints"></param>
        public static void Create(TypePickTotalPointsEntity typePicksTotalPoints)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickTotalPoints = typePicksTotalPoints.MapToBD();

                dbContext.TypePickTotalPoints.Add(dbtypePickTotalPoints);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that updates the current pick of type total points database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickTotalPoints = dbContext.TypePickTotalPoints.Find(IdPick);

                if (dbtypePickTotalPoints == null)
                    throw new NotFoundPickException(IdPick.ToString());

                dbtypePickTotalPoints.IsOver = IsOver;
                dbtypePickTotalPoints.IsUnder = IsUnder;
                dbtypePickTotalPoints.ValueTotalPoints = ValueTotalPoints;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current pick of type total points.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbtypePickTotalPoints = dbContext.TypePickTotalPoints.Find(IdPick);

                if (dbtypePickTotalPoints == null)
                    throw new NotFoundPickException(IdPick.ToString());

                dbContext.TypePickTotalPoints.Remove(dbtypePickTotalPoints);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current pick as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Pick[ {0}, Type: Total Points ]", IdPick);
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
        /// Method that maps a pick of type total points to the database model.
        /// </summary>
        /// <returns></returns>
        internal TypePickTotalPoints MapToBD()
        {
            var dbTypePickTotalPoints = new TypePickTotalPoints
            {
                IdPick = IdPick,
                IsOver = IsOver,
                IsUnder = IsUnder,
                ValueTotalPoints = ValueTotalPoints
            };

            return dbTypePickTotalPoints;
        }

        /// <summary>
        /// Method that maps a pick of type total points from a database model passed by parameter.
        /// </summary>
        /// <param name="dbTypePickTotalPoints"></param>
        /// <returns></returns>
        internal static TypePickTotalPointsEntity MapFromBD(TypePickTotalPoints dbTypePickTotalPoints)
        {
            var typePickTotalPointsEntity = new TypePickTotalPointsEntity()
            {
                IdPick = dbTypePickTotalPoints.IdPick,
                IsOver = dbTypePickTotalPoints.IsOver,
                IsUnder = dbTypePickTotalPoints.IsUnder,
                ValueTotalPoints = dbTypePickTotalPoints.ValueTotalPoints
            };

            return typePickTotalPointsEntity;
        }
        #endregion
    }
}
