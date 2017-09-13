using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class TypePickTotalPointsEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdPick { get; set; }
        public bool? IsOver { get; set; }
        public bool? IsUnder { get; set; }
        public float ValueTotalPoints { get; set; }
        #endregion

        #region Constructor
        public TypePickTotalPointsEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the pick of type total points.
        /// </summary>
        /// <returns></returns>
        public static List<TypePickTotalPointsEntity> Load()
        {
            var typePicksTotalPoints = new List<TypePickTotalPointsEntity>();

            _dbContext.TypePickTotalPoints.ToList().ForEach(x => typePicksTotalPoints.Add(TypePickTotalPointsEntity.Load(x.IdPick)));

            return typePicksTotalPoints;
        }

        /// <summary>
        /// Method that returns the pick of type total points with the Id passed as parameter. 
        /// </summary>
        /// <param name="pickId"></param>
        /// <returns></returns>
        public static TypePickTotalPointsEntity Load(int pickId)
        {
            var dbtypePickTotalPoints = _dbContext.TypePickTotalPoints.Find(pickId);

            if (dbtypePickTotalPoints == null)
                return null;

            return MapFromBD(dbtypePickTotalPoints);
        }

        /// <summary>
        /// Method that adds to the database the past pick of type total points as a parameter.
        /// </summary>
        /// <param name="typePicksTotalPoints"></param>
        public static void Create(TypePickTotalPointsEntity typePicksTotalPoints)
        {
            var dbtypePickTotalPoints = typePicksTotalPoints.MapToBD();

            _dbContext.TypePickTotalPoints.Add(dbtypePickTotalPoints);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that updates the current pick of type total points database.
        /// </summary>
        public void Update()
        {
            var dbtypePickTotalPoints = _dbContext.TypePickTotalPoints.Find(IdPick);

            if (dbtypePickTotalPoints == null)
                throw new NotFoundPickException(IdPick.ToString());

            dbtypePickTotalPoints.IsOver = IsOver;
            dbtypePickTotalPoints.IsUnder = IsUnder;
            dbtypePickTotalPoints.ValueTotalPoints = ValueTotalPoints;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current pick of type total points.
        /// </summary>
        public void Delete()
        {
            var dbtypePickTotalPoints = _dbContext.TypePickTotalPoints.Find(IdPick);

            if (dbtypePickTotalPoints == null)
                throw new NotFoundPickException(IdPick.ToString());

            _dbContext.TypePickTotalPoints.Remove(dbtypePickTotalPoints);
            _dbContext.SaveChanges();
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
            return _dbContext.TypePickTotalPoints.Any(x => x.IdPick == IdPick)
                || _dbContext.TypePickWinner.Any(x => x.IdPick == IdPick);
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
            var typePickTotalPointsEntity = new TypePickTotalPointsEntity(_dbContext)
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
