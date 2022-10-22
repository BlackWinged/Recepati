using Dapper;

namespace Recepati.Code.Models
{
    public class FridgeVsIngredient : BaseObject
    {
        public string FridgeId { get; set; }
        public string IngredientId { get; set; }

        public decimal Size { get; set; }
        public string? Unit { get; set; }
        public string Name { get; set; }


        public FridgeVsIngredient()
        {
            FridgeId = "";
            IngredientId = "";
            Name = "";
        }
    }
}
