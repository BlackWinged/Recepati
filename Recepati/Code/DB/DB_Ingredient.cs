using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class DB_Ingredient
    {
        private PublicConn _pdb { get; set; }
        public DB_Ingredient(PublicConn conn)
        {
            _pdb = conn;
        }

        
        public IEnumerable<Ingredient> GetAll()
        {
            var test = _pdb.conn.GetList<Ingredient>();

            return test;
        }

        public IEnumerable<Ingredient> Search(string query)
        {
            var result = _pdb.conn.GetList<Ingredient>("where name like '%query%'");
            return result;
        }

        public IEnumerable<Ingredient> Save(Ingredient ingredient)
        {
            _pdb.conn.BulkMerge(ingredient);


            return new Ingredient[] { ingredient };
        }


        public IEnumerable<Ingredient> Relations(IEnumerable<Ingredient> ingredients)
        {
            var size = 2000;
            for (var i = 0; i <= ingredients.Count(); i += size)
            {
                var ids = ingredients.Select(x => x.Id);
            }
        }
    }
}