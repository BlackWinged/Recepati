using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class UserManager
    {
        private IHttpContextAccessor context { get; set; }
        public UserManager(IHttpContextAccessor context)
        {
            this.context = context;
        }


        public string CurrentUserId()
        {
            var result = "23d3ce74-78f2-4b99-9615-11d381242fd4";

            return result;
        }

      
    }
}