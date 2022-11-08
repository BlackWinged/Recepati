using Dapper;

namespace Recepati.Code.Models
{
    public class UserLoginModel : BaseObject
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }

    }
}
