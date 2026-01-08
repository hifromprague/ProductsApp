using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProductsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<Context>(options =>
                        options.UseSqlServer(
                            context.Configuration.GetConnectionString("DefaultConnection")
                        ));

                    services.AddTransient<MainForm>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var mainForm = scope.ServiceProvider.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }
    }
}