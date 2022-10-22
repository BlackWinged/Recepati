using Dapper;

namespace Recepati.Code.Models
{
    public class Fridge : BaseObject
    {
        public string? UserId { get; set; }
        public List<FridgeVsIngredient> Contents { get; set; }

        public Fridge()
        {
            Contents = new List<FridgeVsIngredient>();
        }
    }
}
