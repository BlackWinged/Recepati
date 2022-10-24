using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class DB_Recipe
    {
        private PublicConn _pdb { get; set; }
        private UserManager users { get; set; }
        public DB_Recipe(PublicConn conn, UserManager userManager)
        {
            _pdb = conn;
            users = userManager;
        }


        public IEnumerable<Recipe> GetAll()
        {
            var recipes = _pdb.conn.GetList<Recipe>();
            recipes = Relations(recipes);
            return recipes;
        }

        public Recipe Get(string id)
        {
            var recipes = _pdb.conn.Get<Recipe>(id);
            Relations(new List<Recipe>() { recipes });
            return recipes;
        }

        public IEnumerable<Recipe> Search(string query)
        {
            var recipes = _pdb.conn.GetList<Recipe>($"where name like '%{query}%' or description like '%{query}%'");
            recipes = Relations(recipes);
            return recipes;
        }

        public IEnumerable<Recipe> Save(Recipe recipe)
        {
            recipe.UserId = users.CurrentUserId();
            _pdb.conn.BulkMerge(recipe);

            foreach (var ingreedy in recipe.Ingredients)
            {
                ingreedy.RecipeId = recipe.Id;
                //ingreedy.IngredientId = ingreedy.Id;
            }
            _pdb.conn.Execute($"delete from recipeVsIngredient where recipeId = '{recipe.Id}'");

            _pdb.conn.BulkMerge(recipe.Ingredients);
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