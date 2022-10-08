namespace Recepati.Db.Code
{
    public class PublicConn
    {
        private readonly IConfiguration config;
        public PublicConn(IConfiguration config)
        {
            this.config = config;
        }
    }
}
