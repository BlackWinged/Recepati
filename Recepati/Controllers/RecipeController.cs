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
    public class RecipeController : ControllerBase
    {
        private readonly PublicConn _pdb;
        private readonly RecipeManager recipeManager;

        public RecipeController(PublicConn conn, RecipeManager recipeManager)
        {
            _pdb = conn;
            this.recipeManager = recipeManager;
        }

        [HttpGet(Name = "GetAllRecipes")]
        public IEnumerable<Recipe> Get()
        {
            var test = _pdb.conn.GetList<Recipe>();

            return test;
        }

        [HttpPost(Name = "SaveRecipe")]
        public IEnumerable<Recipe> Post(Recipe recipe)
        {
            _pdb.conn.BulkMerge(recipe);
            _pdb.conn.BulkMerge(recipe.Ingredients);
            var rviResult = new List<RecipeVsIngredient>();
            foreach (var ingredient in recipe.Ingredients)
            {
                var RvI = new RecipeVsIngredient();
                RvI.RecipeId = recipe.Id;
                RvI.Id = ingredient.Id;
                rviResult.Add(RvI);
            }
            _pdb.conn.BulkMerge(recipe.Ingredients);


            return new Recipe[] { recipe };
        }

        [Route("~/Recipe/new")]
        public Recipe NewRecipe()
        {
            var newRecipe = new Recipe();

            return newRecipe;
        }
    }
}