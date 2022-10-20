using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class RecipeManager
    {
        private PublicConn _pdb { get; set; }
        private DB_Recipe recipes { get; set; }
        public RecipeManager(PublicConn conn, DB_Recipe recipes)
        {
            _pdb = conn;
            this.recipes = recipes;
        }


        public IEnumerable<Recipe> GetAll()
        {
            var recipes = this.recipes.GetAll();

            return recipes;
        }

        public IEnumerable<Recipe> Search(string query)
        {
            var recipes = this.recipes.Search(query);
            return recipes;
        }

        public IEnumerable<Recipe> Save(Recipe recipe)
        {
            _pdb.conn.BulkMerge(recipe);

            var IvAResult = new List<RecipeVsIngredient>();
            foreach (var ingreedy in recipe.Ingredients)
            {
                var RvI = new RecipeVsIngredient();
                RvI.RecipeId = recipe.Id;
                RvI.IngredientId = ingreedy.Id;
                IvAResult.Add(RvI);
                _pdb.conn.Execute($"delete from recipeVsIngredient where recipeId = {recipe.Id}");
                _pdb.conn.BulkMerge(IvAResult);
            }
            return new Recipe[] { recipe };
        }


        public IEnumerable<Recipe> Relations(IEnumerable<Recipe> recipes)
        {
            List<RecipeVsIngredient> RvI = new List<RecipeVsIngredient>();
            Dictionary<string, Recipe> recipeLookup = recipes.ToDictionary(x => x.Id);
            var size = 2000;
            for (var i = 0; i <= recipes.Count(); i += size)
            {
                var ids = recipes.Skip(i).Take(size).Select(x => x.Id);
                RvI.AddRange(_pdb.conn.GetList<RecipeVsIngredient>(@"where RecipeId in @ids", new { ids }));
            }

            foreach (var item in RvI)
            {
                if (recipeLookup.ContainsKey(item.RecipeId))
                {
                    recipeLookup[item.RecipeId].Ingredients.Add(item);
                }
            }

            return recipes;
        }
    }
}