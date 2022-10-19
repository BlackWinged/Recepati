using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Database;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Managers
{
    public class IngredientManager
    {
        private readonly PublicConn _pdb;
        private readonly DB_Ingredient ingredients;
        public IngredientManager(PublicConn conn, DB_Ingredient ingredients)
        {
            _pdb = conn;
            this.ingredients = ingredients;
        }


        public IEnumerable<Ingredient> Search(string query)
        {
            var result = ingredients.Search(query);

            return result;
        }

        public IEnumerable<Ingredient> GetAll()
        {
            var result = ingredients.GetAll();

            return result;
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