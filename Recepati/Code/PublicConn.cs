using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data.SqlClient;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Recepati.Db.Code
{
    public class PublicConn
    {
        public string ConnectionString { get; set; }
        public PublicConn()
        {
            var dbLocation = Environment.GetEnvironmentVariable("DB_LOCATION");
            var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
            var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
            ConnectionString = $"user id={dbUser};password={dbPass};initial catalog=Recepati;data source={dbLocation};Connect Timeout=2;Max Pool Size=1;MultipleActiveResultSets=True;";


        }

        public virtual ProfiledDbConnection conn
        {
            get
            {
                return new ProfiledDbConnection(getConn(), MiniProfiler.Current);
            }
        }


        public virtual SqlConnection getConn()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
