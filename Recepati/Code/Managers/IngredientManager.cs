using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Managers
{
    public class IngredientManager
    {
        public IngredientManager(PublicConn conn)
        {
            _pdb = conn;
        }

        
        public IEnumerable<Ingredient> GetAllIngredients()
        {
            var test = _pdb.conn.GetList<Ingredient>();

            return test;
        }

        [HttpPost(Name = "SaveIngredient")]
        public IEnumerable<Ingredient> Post(Ingredient ingredient)
        {
            _pdb.conn.BulkMerge(ingredient);


            return new Ingredient[] { ingredient };
        }

        [Route("~/Ingredient/new")]
        public Ingredient NewIngredient()
        {
            var newRecipe = new Ingredient();

            return newRecipe;
        }
    }
}