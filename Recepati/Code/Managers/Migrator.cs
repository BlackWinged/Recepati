using Dapper;
using DbUp;
using Recepati.Db.Code;
using System.Reflection;

namespace Recepati.Code.Models
{
    public class Migrator
    {
        private PublicConn _conn { get; set; }
        public void RunUpgrade()
        {
            var connectionString = _conn.ConnectionString;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(), f => f.Contains("Migrator"))
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            var test = 1;
        }

        public Migrator(PublicConn conn)
        {
            _conn = conn;
        }

    }
}
