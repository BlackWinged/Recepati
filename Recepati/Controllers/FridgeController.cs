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
    public class FridgeController : ControllerBase
    {
        private readonly PublicConn _pdb;
        private readonly FridgeManager fridgeManager;
        private readonly UserManager userManager;

        public FridgeController(PublicConn conn, FridgeManager recipeManager, UserManager userManager)
        {
            _pdb = conn;
            this.fridgeManager = recipeManager;
            this.userManager = userManager;
        }

        [HttpGet(Name = "GetFridgeForUser")]
        [Route("~/fridge/currentuser/")]
        public Fridge GetRecipe()
        {
            var fridge = fridgeManager.GetForUser(userManager.CurrentUserId());

            if (fridge == null)
            {
                fridge = new Fridge();
            }

            return fridge;
        }


        [Route("~/fridge/savecurrentuser/")]
        [HttpPost(Name = "SaveFridge")]
        public IEnumerable<Fridge> Post(Fridge fridge)
        {
            fridgeManager.Save(fridge);

            return new Fridge[] { fridge };
        }

        [Route("~/fridge/Ingredient/new")]
        public FridgeVsIngredient NewFridgeIngredient()
        {
            var newFridge = new FridgeVsIngredient();

            return newFridge;
        }
    }
}