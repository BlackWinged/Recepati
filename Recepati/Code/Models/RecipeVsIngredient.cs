using Dapper;

namespace Recepati.Code.Models
{
    public class RecipeVsIngredient : BaseObject
    {
        public string RecipeId { get; set; }
        public string IngredientId { get; set; }

        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public string Name { get; set; }


        public RecipeVsIngredient()
        {
            RecipeId = "";
            IngredientId = "";
            Name = "";
        }
    }
}
