using Dapper;
using Microsoft.AspNetCore.Mvc;
using Recepati.Code.Models;
using Recepati.Db.Code;
using Z.Dapper.Plus;

namespace Recepati.Database
{
    public class DB_Ingredient
    {
        private PublicConn _pdb { get; set; }
        public DB_Ingredient(PublicConn conn)
        {
            _pdb = conn;
        }


        public IEnumerable<Ingredient> GetAll()
        {
            var ingredients = _pdb.conn.GetList<Ingredient>();
            ingredients = Relations(ingredients);
            return ingredients;
        }

        public IEnumerable<Ingredient> Search(string query)
        {
            var ingredients = _pdb.conn.GetList<Ingredient>($"where name like '%{query}%'");
            ingredients = Relations(ingredients);
            return ingredients;
        }

        public IEnumerable<Ingredient> Save(Ingredient ingredient)
        {
            _pdb.conn.BulkMerge(ingredient);

            var IvAResult = new List<IngredientVsAlternative>();
            foreach (var alternative in ingredient.Alternatives)
            {
                var IvA = new IngredientVsAlternative();
                IvA.IngredientId1 = ingredient.Id;
                IvA.IngredientId2 = alternative.Id;
                IvAResult.Add(IvA);
                _pdb.conn.BulkMerge(IvAResult);
            }
            return new Ingredient[] { ingredient };
        }


        public IEnumerable<Ingredient> Relations(IEnumerable<Ingredient> ingredients)
        {
            List<IngredientVsAlternative> IvA = new List<IngredientVsAlternative>();
            Dictionary<string, Ingredient> prodLookup = ingredients.ToDictionary(x => x.Id);
            var size = 2000;
            for (var i = 0; i <= ingredients.Count(); i += size)
            {
                var ids = ingredients.Skip(i).Take(size).Select(x => x.Id);
                IvA.AddRange(_pdb.conn.GetList<IngredientVsAlternative>(@"where IngredientId1 in @ids", new { ids }));
            }

            for (var i = 0; i <= IvA.Count(); i += size)
            {
                var bufferIds = IvA.Skip(i).Take(size).ToList();
                var ids = bufferIds.Select(x => x.IngredientId2);
                var alt = _pdb.conn.GetList<Ingredient>("where Id in @ids", new { ids });
                var altLookup = alt.ToDictionary(x => x.Id);

                for (var j = 0; j <= bufferIds.Count(); j++)
                {
                    if (prodLookup.ContainsKey(bufferIds[j].IngredientId1)){
                        if (altLookup.ContainsKey(bufferIds[j].IngredientId2))
                        {
                            prodLookup[bufferIds[j].IngredientId1].Alternatives.Add(altLookup[bufferIds[j].IngredientId2]);                        }
                    }
                }
            }

            return ingredients;
        }
    }
}