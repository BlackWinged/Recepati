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
        public IEnumerable<Recipe> Get(string? query)
        {
            var recipes = new List<Recipe>();
            if (query != null)
                recipes = recipeManager.Search(query).ToList();
            else
                recipes = recipeManager.GetAll().ToList();
            return recipes;
        }

        [Route("~/Recipe/byId/{id}")]
        public Recipe GetRecipe(string id)
        {
            var recipe = new Recipe();
            recipe = recipeManager.Get(id);
            return recipe;
        }


        [HttpPost(Name = "SaveRecipe")]
        public IEnumerable<Recipe> Post(Recipe recipe)
        {
            recipeManager.Save(recipe);

            return new Recipe[] { recipe };
        }

        [Route("~/Recipe/new")]
        public Recipe NewRecipe()
        {
            var newRecipe = new Recipe();

            return newRecipe;
        }

        [Route("~/Recipe/Ingredient/new")]
        public RecipeVsIngredient NewRecipeIngredient()
        {
            var newRecipe = new RecipeVsIngredient();

            return newRecipe;
        }
    }
}