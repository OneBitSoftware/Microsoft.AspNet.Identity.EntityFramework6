using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework6.Test.Utilities
{
    public static class DbUtil
    {
        public static IServiceCollection ConfigureDbServices(string connectionString, IServiceCollection services = null)
        {
            return ConfigureDbServices<IdentityDbContext>(connectionString, services);
        }

        public static IServiceCollection ConfigureDbServices<TContext>(string connectionString,
            IServiceCollection services = null) where TContext : DbContext
        {
            if (services == null)
            {
                services = new ServiceCollection();
            }
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddEntityFramework().AddSqlServer().AddDbContext<TContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IdentityDbContext>(context =>
                new IdentityDbContext(connectionString));

            return services;
        }

        public static TContext Create<TContext>(string connectionString) where TContext : DbContext, new()
        {
            var serviceCollection = ConfigureDbServices<TContext>(connectionString);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetRequiredService<TContext>();
        }
    }

}
