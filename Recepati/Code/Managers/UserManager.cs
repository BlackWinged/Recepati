using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using System.Text.RegularExpressions;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class UserManager
    {
        private IHttpContextAccessor context { get; set; }
        private DB_User users;
        private SecurityManager secManager;
        public UserManager(IHttpContextAccessor context, DB_User users, SecurityManager secManager)
        {
            this.context = context;
            this.users = users;
            this.secManager = secManager;   
        }


        public User? Register(User user)
        {
            user.Password = secManager.HashPassword(user.Password);
            users.Register(user);
            return user;
        }

        public User? LogIn(User user)
        {
            if (!string.IsNullOrEmpty(user.Mail))
            {
                var sentPassword = user.Password;
                user = users.GetByMail(user.Mail);

                if (!secManager.ValidatePassword(sentPassword, user.Password))
                {
                    user = null;
                }
            }

            return user;
        }

        public string GenerateToken(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user ne postoji ili je kriva šifra");
            }

            var token = secManager.GenerateToken(user);

            return token;
        }

        public User GetByMail(string mail)
        {
            var result = users.GetByMail(mail);

            return result;
        }

        public string CurrentUserId()
        {
            var result = "23d3ce74-78f2-4b99-9615-11d381242fd4";

            return result;
        }



    
    }
}