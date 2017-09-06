using System;
using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    class TypePickWinnerEntity
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Attributes
        public int IdPick { get; set; }
        public bool? HasHandicap { get; set; }
        public float? ValueHandicap { get; set; }
        public int IdWinnerTeam { get; set; }
        #endregion

        #region Constructor
        public TypePickWinnerEntity(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the pick of type winner.
        /// </summary>
        /// <returns></returns>
        public static List<TypePickWinnerEntity> Load()
        {
            var typePicksWinner = new List<TypePickWinnerEntity>();

            _dbContext.TypePickWinner.ToList().ForEach(x => typePicksWinner.Add(TypePickWinnerEntity.Load(x.IdPick)));

            return typePicksWinner;
        }

        /// <summary>
        /// Method that returns the pick of type winner with the Id passed as parameter. 
        /// </summary>
        /// <param name="pickId"></param>
        /// <returns></returns>
        public static TypePickWinnerEntity Load(int pickId)
        {
            var dbTypePickWinner = _dbContext.TypePickWinner.Find(pickId);

            if (dbTypePickWinner == null)
                return null;

            return MapFromBD(dbTypePickWinner);
        }

        /// <summary>
        /// Method that adds to the database the past pick of type winner as a parameter.
        /// </summary>
        /// <param name="typePickWinner"></param>
        public static void Create(TypePickWinnerEntity typePickWinner)
        {
            var dbtypePickWinner = typePickWinner.MapToBD();

            _dbContext.TypePickWinner.Add(dbtypePickWinner);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that updates the current pick of type winner database.
        /// </summary>
        public void Update()
        {
            var dbtypePickWinner = _dbContext.TypePickWinner.Find(IdPick);

            if (dbtypePickWinner == null)
                throw new NotFoundPickException(IdPick.ToString());

            dbtypePickWinner.HasHandicap = HasHandicap;
            dbtypePickWinner.ValueHandicap = ValueHandicap;
            dbtypePickWinner.IdWinnerTeam = IdWinnerTeam;

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Method that removes of database the current pick of type winner.
        /// </summary>
        public void Delete()
        {
            var dbtypePickWinner = _dbContext.TypePickWinner.Find(IdPick);

            if (dbtypePickWinner == null)
                throw new NotFoundPickException(IdPick.ToString());

            _dbContext.TypePickWinner.Remove(dbtypePickWinner);
            _dbContext.SaveChanges();
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
            return _dbContext.TypePickTotalPoints.Any(x => x.IdPick == IdPick) 
                || _dbContext.TypePickWinner.Any(x => x.IdPick == IdPick);
        }

        /// <summary>
        /// Method that maps a pick of type winner to the database model.
        /// </summary>
        /// <returns></returns>
        internal TypePickWinner MapToBD()
        {
            var dbTypePickWinner = new TypePickWinner();

            dbTypePickWinner.IdPick = IdPick;
            dbTypePickWinner.HasHandicap = HasHandicap;
            dbTypePickWinner.ValueHandicap = ValueHandicap;
            dbTypePickWinner.IdWinnerTeam = IdWinnerTeam;

            return dbTypePickWinner;
        }

        /// <summary>
        /// Method that maps a pick of type winner from a database model passed by parameter.
        /// </summary>
        /// <param name="dbTypePickWinner"></param>
        /// <returns></returns>
        internal static TypePickWinnerEntity MapFromBD(TypePickWinner dbTypePickWinner)
        {
            var typePickWinnerEntity = new TypePickWinnerEntity(_dbContext);

            typePickWinnerEntity.IdPick = dbTypePickWinner.IdPick;
            typePickWinnerEntity.HasHandicap = dbTypePickWinner.HasHandicap;
            typePickWinnerEntity.ValueHandicap = dbTypePickWinner.ValueHandicap;
            typePickWinnerEntity.IdWinnerTeam = dbTypePickWinner.IdWinnerTeam;

            return typePickWinnerEntity;
        }
        #endregion
    }
}
