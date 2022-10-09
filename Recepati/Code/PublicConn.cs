﻿using StackExchange.Profiling;
using StackExchange.Profiling.Data;
using System.Data.SqlClient;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Recepati.Db.Code
{
    public class PublicConn
    {
        private readonly IConfiguration config;
        public string ConnectionString { get; set; }
        public PublicConn(IConfiguration config)
        {
            this.config = config;

            var dbLocation = Environment.GetEnvironmentVariable("DB_LOCATION");
            var dbUser = Environment.GetEnvironmentVariable("DB_USERNAME");
            var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");
            ConnectionString = $"user id={dbUser};password={dbPass};initial catalog=Recepati;data source={dbLocation};Connect Timeout=900;Max Pool Size=400;MultipleActiveResultSets=True;";


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
