using Dapper;

namespace Recepati.Code.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("Procedure")]
        public string procedureSeralized { get; set; }
        public List<string> Procedure { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public Recipe()
        {
            Procedure = new List<string>();
            Name = "";
            Ingredients = new List<Ingredient>();
            Description = "";
            procedureSeralized = "";
        }
    }
}
