using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class DB_Fridge
    {
        private PublicConn _pdb { get; set; }
        public DB_Fridge(PublicConn conn)
        {
            _pdb = conn;
        }


        public IEnumerable<Fridge> GetAll()
        {
            var ingredients = _pdb.conn.GetList<Fridge>();
            ingredients = Relations(ingredients);
            return ingredients;
        }

        public Fridge Get(string id)
        {
            var fridge = _pdb.conn.Get<Fridge>(id);
            Relations(new List<Fridge> { fridge });
            return fridge;
        }

        public Fridge? GetForUser(string userId)
        {
            var fridge = _pdb.conn.GetList<Fridge>("where userid = @userId", new { userId });
            Relations(fridge);
            return fridge.FirstOrDefault();
        }


        public IEnumerable<Fridge> Save(Fridge fridge)
        {
            _pdb.conn.BulkMerge(fridge);

            var FvIResult = new List<FridgeVsIngredient>();
            foreach (var item in fridge.Contents)
            {
                var FvI = new FridgeVsIngredient();

                FvI.FridgeId = item.Id;
                FvIResult.Add(FvI);
            }
            _pdb.conn.Execute($"delete from FridgeVsIngredient where FridgeId = '{fridge.Id}'");
            _pdb.conn.BulkMerge(FvIResult);

            return new Fridge[] { fridge };
        }


        public IEnumerable<Fridge> Relations(IEnumerable<Fridge> fridges)
        {
            List<FridgeVsIngredient> RvI = new List<FridgeVsIngredient>();
            Dictionary<string, Fridge> recipeLookup = fridges.ToDictionary(x => x.Id);
            var size = 2000;
            for (var i = 0; i <= fridges.Count(); i += size)
            {
                var ids = fridges.Skip(i).Take(size).Select(x => x.Id);
                RvI.AddRange(_pdb.conn.GetList<FridgeVsIngredient>(@"where FridgeId in @ids", new { ids }));
            }

            foreach (var item in RvI)
            {
                if (recipeLookup.ContainsKey(item.FridgeId))
                {
                    recipeLookup[item.FridgeId].Contents.Add(item);
                }
            }

            return fridges;
        }
    }
}