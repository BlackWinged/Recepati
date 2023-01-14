using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using Recepati.Managers;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class MeasurementUnitManager
    {
        private PublicConn _pdb { get; set; }
        public MeasurementUnitManager(PublicConn conn, DB_Fridge fridges, UserManager userManager, IngredientManager ingredientManager)
        {
            _pdb = conn;
            this.fridges = fridges;
            this.userManager = userManager;
            this.ingredientManager = ingredientManager;
        }


        public IEnumerable<Fridge> GetAll()
        {
            var recipes = this.fridges.GetAll();

            return recipes;
        }

        public Fridge Get(string id)
        {
            var fridge = this.fridges.Get(id);

            return fridge;
        }

        public IEnumerable<Fridge> Save(Fridge fridge)
        {
            fridge.UserId = userManager.CurrentUserId();
            this.fridges.Save(fridge);
            return new Fridge[] { fridge };
        }

        public IEnumerable<FridgeVsIngredient> SaveIngredient(FridgeVsIngredient fridgeIngredient)
        {

            var newIngredient = new Ingredient();
            newIngredient.Name = fridgeIngredient.Name;

            ingredientManager.Save(newIngredient);
            fridgeIngredient.IngredientId = newIngredient.Id;

            var fridge = GetForUser(userManager.CurrentUserId());
            if (fridge != null)
            {
                fridge.Contents.Add(fridgeIngredient);
                this.fridges.Save(fridge);
            }

            return new FridgeVsIngredient[] { fridgeIngredient };
        }

        public Fridge? GetForUser(string userId)
        {
            var fridge = fridges.GetForUser(userId);

            return fridge;
        }
    }
}