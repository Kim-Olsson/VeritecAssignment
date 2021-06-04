using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using VeritecAssignment.Repository.StaticRepository;
using VeritecAssignment.ConsoleApplication;

namespace VeritecAssignment.Business.SalaryPackage
{
    class Program
    {
        static void Main(string[] args)
        {
            using var scope = ConfigureServices(args);
            IApplication app = scope.Services.GetRequiredService<IApplication>();
            app.Start();
        }
        static IHost ConfigureServices(string[] args)
        {
            IHost builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddScoped<ISalaryPackageCalculator, SalaryPackageCalculator>()
                            .AddScoped<ISalaryPackageRule, SalaryPackageRule>()
                            .AddScoped<IRateRepository, StaticRateRepository>()
                            .AddScoped<IApplication, Application>())
                .Build();
            return (builder);
        }
    }
}
