using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class DB_User
    {
        private PublicConn _pdb { get; set; }
        public DB_User(PublicConn conn)
        {
            _pdb = conn;
        }


        public User GetByMail(string mail)
        {
            var user = _pdb.conn.GetList<User>("where mail = @mail", new { mail }).Single();

            return user;
        }

        public User? Register(User user)
        {
            _pdb.conn.BulkInsert(user);
            return user;
        }
    }
}