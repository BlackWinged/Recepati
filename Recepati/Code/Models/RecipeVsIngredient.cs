using Dapper;

namespace Recepati.Code.Models
{
    public class RecipeVsIngredient : BaseObject
    {
        public string? RecipeId { get; set; }
        public string? IngredientId { get; set; }

    }
}
