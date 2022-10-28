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

        public UserController(SecurityManager security)
        {
            this.security = security;
        }

        [Route("~/User/")]
        public string Get(string? query)
        {
            var token = security.GenerateToken();
            return token;
        }

        
        [Route("~/User/TestParsed")]
        public string Parsed(string? query)
        {
            var token = security.GenerateToken();

            var result = security.ParseToken(token);

            return result;
        }

    }
}