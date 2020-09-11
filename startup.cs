using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using AlexPagnotta.Function;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AlexPagnotta.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            // this will bind to the "Values" section of the configuration
            builder
                .Services
                .AddOptions<Settings>()
                .Configure<IConfiguration>((settings, configuration) => { configuration.Bind(settings); });

            // this will bind to a specific section of the configuration
            // by using the section name, in this case "ConnectionStrings"
            /*builder
                .Services
                .AddOptions<ConnectionStrings>()
                .Configure<IConfiguration>((settings, configuration) => { configuration.Bind("ConnectionStrings", settings); });*/

        }
    }
}