using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Recepati.Managers;
using Z.Dapper.Plus;

namespace Recepati.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly PublicConn _pdb;
        private readonly IngredientManager ingredients;

        public IngredientController(PublicConn conn, IngredientManager ingredients)
        {
            _pdb = conn;
            this.ingredients = ingredients;
        }

        [HttpGet(Name = "GetAllIngredients")]
        public IEnumerable<Ingredient> Get(string? query = null)
        {
            if (query == null)
            {
                var allIngredients = ingredients.GetAll();
                return allIngredients;
            }
            else
            {
                var searchedIngredients = ingredients.Search(query);
                return searchedIngredients;
            }

        }

        [HttpPost(Name = "SaveIngredient")]
        public IEnumerable<Ingredient> Post(Ingredient ingredient)
        {
            ingredients.Save(ingredient);
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