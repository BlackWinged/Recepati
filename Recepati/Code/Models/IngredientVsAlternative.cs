using Dapper;

namespace Recepati.Code.Models
{
    public class IngredientVsAlternative : BaseObject
    {
        public string IngredientId1 { get; set; }
        public string IngredientId2 { get; set; }

        public IngredientVsAlternative()
        {
            IngredientId1 = "";
            IngredientId2 = "";
        }
    }
}
