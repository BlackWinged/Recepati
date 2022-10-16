using Dapper;

namespace Recepati.Code.Models
{
    public class Ingredient: BaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Alternatives { get; set; }
        public decimal Price { get; set; }

        public Ingredient()
        {
            Name = "";
            Description = "";
            Alternatives = new List<Ingredient>();
        }
    }
}
