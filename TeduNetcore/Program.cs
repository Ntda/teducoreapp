using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TeduNetcore.Data.EF;

namespace TeduNetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = BuildWebHost(args);
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                try
                {
                    DbInitializer dbInitializer = serviceProvider.GetService<DbInitializer>();
                    Task.WaitAll(dbInitializer.Seed());
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetService<ILogger<Program>>();
                    logger.LogError("An error occurred while seed database: {0}", ex.Message);
                    throw;
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
.UseStartup<Startup>()
.Build();
        }
    }
}
