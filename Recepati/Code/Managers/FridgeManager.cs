using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Controllers;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class FridgeManager
    {
        private PublicConn _pdb { get; set; }
        private DB_Fridge fridges { get; set; }
        public FridgeManager(PublicConn conn, DB_Fridge fridges)
        {
            _pdb = conn;
            this.fridges = fridges;
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
            this.fridges.Save(fridge);
            return new Fridge[] { fridge };
        }

        public Fridge? GetForUser(string userId)
        {
            var fridge = fridges.GetForUser(userId);

            return fridge;
        }
    }
}