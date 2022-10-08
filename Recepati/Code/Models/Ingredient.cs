using Dapper;

namespace Recepati.Code.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public List<Ingredient> Alternatives { get; set; }
        public decimal Price { get; set; }
    }
}
