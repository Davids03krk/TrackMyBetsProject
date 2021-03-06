﻿using System.Collections.Generic;
using System.Linq;
using TrackMyBets.Data.Models;
using TrackMyBets.Business.Exceptions;

namespace TrackMyBets.Business.Entities
{
    public class RelUserBookmakerEntity
    {
        #region Attributes
        public int IdRelUserBookmaker { get; set; }
        public decimal Bankroll { get; set; }
        public int IdUser { get; set; }
        public int IdBookmaker { get; set; }
        #endregion
        
        #region Public Methods
        /// <summary>
        /// Method that returns a list with all the rels_user_bookmaker.
        /// </summary>
        /// <returns></returns>
        public static List<RelUserBookmakerEntity> Load()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var relsUserBookmaker = new List<RelUserBookmakerEntity>();

                dbContext.RelUserBookmaker.ToList().ForEach(x => relsUserBookmaker.Add(RelUserBookmakerEntity.Load(x.IdRelUserBookmaker)));

                return relsUserBookmaker;
            }
        }

        /// <summary>
        /// Method that returns the rels_user_bookmaker with the Id passed as parameter. 
        /// </summary>
        /// <param name="relUserBookmakerId"></param>
        /// <returns></returns>
        public static RelUserBookmakerEntity Load(int relUserBookmakerId)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbRelUserBookmaker = dbContext.RelUserBookmaker.Find(relUserBookmakerId);

                if (dbRelUserBookmaker == null)
                    return null;

                return MapFromBD(dbRelUserBookmaker);
            }
        }

        /// <summary>
        /// Method that returns a list of rels_user_bookmaker of a certain user passed as parameter.
        /// </summary>
        /// <returns></returns>
        public static List<RelUserBookmakerEntity> Load(UserEntity user)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var relsUserBookmaker = new List<RelUserBookmakerEntity>();

                dbContext.RelUserBookmaker.ToList().ForEach(x =>
                {
                    if (x.IdUser == user.IdUser)
                        relsUserBookmaker.Add(RelUserBookmakerEntity.Load(x.IdRelUserBookmaker));
                });

                return relsUserBookmaker;
            }
        }

        /// <summary>
        /// Method that returns a list of rels_user_bookmaker of a certain bookmaker passed as parameter.
        /// </summary>
        /// <returns></returns>
        public static List<RelUserBookmakerEntity> Load(BookmakerEntity bookmaker)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var relsUserBookmaker = new List<RelUserBookmakerEntity>();

                dbContext.RelUserBookmaker.ToList().ForEach(x =>
                {
                    if (x.IdBookmaker == bookmaker.IdBookmaker)
                        relsUserBookmaker.Add(RelUserBookmakerEntity.Load(x.IdRelUserBookmaker));
                });

                return relsUserBookmaker;
            }
        }

        /// <summary>
        /// Method that adds to the database the past rel_user_bookmaker as a parameter.
        /// </summary>
        /// <param name="relUserBookmaker"></param>
        public static void Create(RelUserBookmakerEntity relUserBookmaker)
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                if (relUserBookmaker.Exist())
                    throw new DuplicatedRelUserBookmakerException(relUserBookmaker.ToString());

                var dbRelUserBookmaker = relUserBookmaker.MapToBD();
                dbContext.RelUserBookmaker.Add(dbRelUserBookmaker);
                dbContext.SaveChanges();

                relUserBookmaker.IdRelUserBookmaker = dbRelUserBookmaker.IdRelUserBookmaker;
            }
        }

        /// <summary>
        /// Method that updates the current rel_user_bookmaker database.
        /// </summary>
        public void Update()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbRelUserBookmaker = dbContext.RelUserBookmaker.Find(IdRelUserBookmaker);

                if (dbRelUserBookmaker == null)
                    throw new NotFoundRelUserBookmakerException(IdRelUserBookmaker.ToString());

                dbRelUserBookmaker.Bankroll = Bankroll;
                dbRelUserBookmaker.IdUser = IdUser;
                dbRelUserBookmaker.IdBookmaker = IdBookmaker;

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that removes of database the current rel_user_bookmaker.
        /// </summary>
        public void Delete()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                var dbRelUserBookmaker = dbContext.RelUserBookmaker.Find(IdRelUserBookmaker);

                if (dbRelUserBookmaker == null)
                    throw new NotFoundRelUserBookmakerException(IdRelUserBookmaker.ToString());

                BetEntity.Load(this).ForEach(x => x.Delete());
                IncomeEntity.Load(this).ForEach(x => x.Delete());
                WithdrawalEntity.Load(this).ForEach(x => x.Delete());

                dbContext.RelUserBookmaker.Remove(dbRelUserBookmaker);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Method that returns the current rel_user_bookmaker as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("RelUserBookmaker[ User: {0} , Bookmaker: {1} ]", IdUser, IdBookmaker);
        }
        #endregion

        #region Internal Methods        
        /// <summary>
        /// Method that returns if the current rel_user_bookmaker exists in the database.
        /// </summary>
        /// <returns></returns>
        internal bool Exist()
        {
            using (var dbContext = new BD_TRACKMYBETSContext())
            {
                return dbContext.RelUserBookmaker.Any(x => x.IdUser == IdUser && x.IdBookmaker == IdBookmaker);
            }
        }

        /// <summary>
        /// Method that maps a rel_user_bookmaker to the database model.
        /// </summary>
        /// <returns></returns>
        internal RelUserBookmaker MapToBD()
        {
            var dbRelUserBookmaker = new RelUserBookmaker
            {
                Bankroll = Bankroll,
                IdUser = IdUser,
                IdBookmaker = IdBookmaker
            };

            return dbRelUserBookmaker;
        }

        /// <summary>
        /// Method that maps a rel_user_bookmaker from a database model passed by parameter.
        /// </summary>
        /// <param name="dbRelUserBookmaker"></param>
        /// <returns></returns>
        internal static RelUserBookmakerEntity MapFromBD(RelUserBookmaker dbRelUserBookmaker)
        {
            var relUserBookmakerEntity = new RelUserBookmakerEntity()
            {
                IdRelUserBookmaker = dbRelUserBookmaker.IdRelUserBookmaker,
                Bankroll = dbRelUserBookmaker.Bankroll,
                IdUser = dbRelUserBookmaker.IdUser,
                IdBookmaker = dbRelUserBookmaker.IdBookmaker
            };

            return relUserBookmakerEntity;
        }
        #endregion
    }
}
