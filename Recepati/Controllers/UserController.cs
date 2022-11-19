using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Database;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SecurityManager security;
        private readonly UserManager userManager;

        public UserController(SecurityManager security, UserManager userManager)
        {
            this.security = security;
            this.userManager = userManager;
        }

        //[Route("~/User/")]
        //public string Get(string? query)
        //{
        //    var token = security.GenerateToken();
        //    return token;
        //}

        
        //[Route("~/User/TestParsed")]
        //public string Parsed(string? query)
        //{
        //    var token = security.GenerateToken();

        //    var result = security.ParseToken(token);

        //    return result;
        //}

        [Route("~/User/LogIn")]
        public string LogIn(User user)
        {
            var loggedInUser = userManager.LogIn(user);
            var resultToken = "";
            if (loggedInUser != null)
            {
                resultToken = security.GenerateToken(loggedInUser);
                Response.Cookies.Append("AuthMedo", resultToken);
                return resultToken;
            }


            Response.StatusCode = 403;
            return "Nemoze.";
        }

        [Route("~/User/Register")]
        public string Register(User user)
        {

            userManager.Register(user);
            var loggedInUser = userManager.LogIn(user);
            var resultToken = "";
            if (loggedInUser != null)
            {
                resultToken = security.GenerateToken(loggedInUser);
                Response.Cookies.Append("Auth:Medo", resultToken);
                return resultToken;
            }


            Response.StatusCode = 403;
            return "Nemoze.";
        }

        [Route("~/User/Validate")]
        public string ValidateToken(string token)
        {
            var parsedToken = security.ParseToken(token);

            return "Success";
        }

    }
}