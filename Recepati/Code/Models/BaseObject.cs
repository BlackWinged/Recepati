using Dapper;

namespace Recepati.Code.Models
{
    public class BaseObject
    {
        private string _id { get; set; }
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public BaseObject()
        {
            if (_id == null)
            {
                _id = Guid.NewGuid().ToString();
            }
        }
    }
}
