﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Business.Functions;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class TypePickWinnerModel : TypePickModel
    {
        #region DBContext
        private static BD_TRACKMYBETSContext _dbContext;
        #endregion

        #region Constructor
        public TypePickWinnerModel(BD_TRACKMYBETSContext dbCOntext)
        {
            _dbContext = dbCOntext;
        }
        #endregion

        #region Properties
        public bool? HasHandicap { get; set; }

        public float? ValueHandicap { get; set; }

        [Required]
        public TeamModel WinnerTeam { get; set; }
        #endregion

        #region Public Methods
        public static TypePickWinnerModel FromEntity(TypePickWinnerEntity typePickWinner)
        {
            TypePickWinnerModel typePickWinnerModel = new TypePickWinnerModel(_dbContext)
            {
                IdPick = typePickWinner.IdPick,
                HasHandicap = typePickWinner.HasHandicap,
                ValueHandicap = typePickWinner.ValueHandicap,
                WinnerTeam = TeamModel.FromEntity(TeamEntity.Load(typePickWinner.IdWinnerTeam)),
                TypePick = Enumerators.PickTypes.WINNER
            };

            return typePickWinnerModel;
        }

        public TypePickWinnerEntity ToEntities()
        {
            var typePickWinnerEntity = new TypePickWinnerEntity(_dbContext);

            typePickWinnerEntity.IdPick = this.IdPick;
            typePickWinnerEntity.HasHandicap = this.HasHandicap;
            typePickWinnerEntity.ValueHandicap = this.ValueHandicap;
            typePickWinnerEntity.IdWinnerTeam = this.WinnerTeam.IdTeam;

            return typePickWinnerEntity;
        }
        #endregion
    }
}
