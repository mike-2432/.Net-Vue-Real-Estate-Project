using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Main.DTO;
using server.Main.Models;

namespace server.Main.Service
{
    public interface IAuthService
    {
        /// <summary>
        /// This method validates the registration and registers a new users
        /// </summary>
        /// <param name = "user" >The user object</param>
        /// <param name = "password" >The password string</param>
        /// <returns>Returns a serviceResponseDto without a data object</returns>
        Task<ServiceResponseDto<string>> Register(User user, string password);

        /// <summary>
        /// This method returns a jwt token after successful login
        /// </summary>
        /// <param name = "user" >The username string</param>
        /// <param name = "password" >The password string</param>
        /// <returns>Returns a serviceResponseDto with in the data the jwt token</returns>
        Task<ServiceResponseDto<string>> Login(string username, string password);

        /// <summary>
        /// This method validates the new password and validates it against the old password
        /// After a successful validation, the password is changed in the database
        /// </summary>
        /// <param name = "oldPassword" >The old password</param>
        /// <param name = "newPassword" >The the new password</param>
        /// <returns>Returns a serviceResponseDto without a data object</returns>
        Task<ServiceResponseDto<string>> ChangePassword(string oldPassword, string newPassword);

        /// <summary>
        /// This method checks if the user exists in the database
        /// </summary>
        /// <param name = "username" >The username</param>
        /// <returns>Returns whether a user exists</returns>
        Task<bool> UserExists(string username);
    }
}