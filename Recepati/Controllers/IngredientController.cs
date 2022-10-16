using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly PublicConn _pdb;

        public IngredientController(PublicConn conn)
        {
            _pdb = conn;
        }

        [HttpGet(Name = "GetAllIngredients")]
        public IEnumerable<Ingredient> Get()
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