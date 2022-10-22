using Dapper;

namespace Recepati.Code.Models
{
    public class User : BaseObject
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }

    }
}
