﻿using System.ComponentModel.DataAnnotations;
using TrackMyBets.Business.Entities;
using TrackMyBets.Data.Models;

namespace TrackMyBets.Model
{
    public class UserModel
    {
        #region Properties
        public int IdUser { get; set; }

        [Required]
        public string Nick { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string SurnameFirst { get; set; }

        public string SurnameSecond { get; set; }

        [Required]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }
        #endregion

        #region Public Methods
        public static UserModel FromEntity(UserEntity user)
        {
            UserModel userModel = new UserModel()
            {
                IdUser = user.IdUser,
                Nick = user.Nick,
                Password = user.Password,
                Name = user.Name,
                SurnameFirst = user.SurnameFirst,
                SurnameSecond = user.SurnameSecond,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };

            return userModel;
        }

        public UserEntity ToEntities()
        {
            var userEntity = new UserEntity()
            {
                IdUser = IdUser,
                Nick = Nick,
                Password = Password,
                Name = Name,
                SurnameFirst = SurnameFirst,
                SurnameSecond = SurnameSecond,
                Email = Email,
                Phone = Phone,
                Address = Address
            };

            return userEntity;
        }
        #endregion
    }

    public class UserLogin {
        [Required]
        public string Nick { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserAuth {
        public int IdUser { get; set; }

        public string Nick { get; set; }

        public string Token { get; set; }
    }
}
