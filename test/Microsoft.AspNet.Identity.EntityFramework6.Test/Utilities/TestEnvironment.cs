using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework6.Test.Utilities
{
    public static class TestEnvironment
    {
        public static IConfiguration Config { get; }

        public static string TestIdentityDbConnectionString = "Test:SqlServer:ConnectionString";

        static TestEnvironment()
        {
            var configBuilder = new ConfigurationBuilder()
                .AddJsonFile("config.json");

            Config = configBuilder.Build();
        }
    }
}
