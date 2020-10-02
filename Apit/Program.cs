using DatabaseLayer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Apit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // // Init test data in database (remove it on Production) 
            // using (var scope = host.Services.CreateScope())
            // {
            //     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            //     TestData.ClearDatabase(context);
            //
            //     // TestData.Apply(context);
            // }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}