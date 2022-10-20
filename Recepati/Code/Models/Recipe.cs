using Dapper;
using System.Text.Json.Serialization;

namespace Recepati.Code.Models
{
    public class Recipe: BaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Column("Procedure")]
        [JsonIgnore]
        public string procedureSeralized { get; set; }
        public List<string> Procedure { get; set; }
        public List<RecipeVsIngredient> Ingredients { get; set; }
        public string Url { get; set; }

        public Recipe()
        {
            Procedure = new List<string>();
            Name = "";
            Ingredients = new List<RecipeVsIngredient>();
            Description = "";
            procedureSeralized = "";
            Url = "";
        }
    }
}
