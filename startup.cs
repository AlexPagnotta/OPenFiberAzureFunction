using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using AlexPagnotta.Function;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AlexPagnotta.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<Settings>(s=>
            {
                s.OpenFiberAddress = Environment.GetEnvironmentVariable("OpenFiberAddress");
                s.OpenFiberAddressUrl = Environment.GetEnvironmentVariable("OpenFiberAddressUrl");
                s.FWACoverageStringIdentifier = Environment.GetEnvironmentVariable("FWACoverageStringIdentifier");
                s.FTTHCoverageStringIdentifier = Environment.GetEnvironmentVariable("FTTHCoverageStringIdentifier");
                s.NOCoverageStringIdentifier = Environment.GetEnvironmentVariable("NOCoverageStringIdentifier");
                s.ParentElementIdentifier = Environment.GetEnvironmentVariable("ParentElementIdentifier");
                s.CoverageStringElementIdentifier = Environment.GetEnvironmentVariable("CoverageStringElementIdentifier");
            });

            builder.Services.Configure<SendGridSettings>(sg=>
            {
                sg.API_KEY = Environment.GetEnvironmentVariable("SendGrid:API_KEY");  

                sg.Sender = new SendGridEmailUser(
                        Environment.GetEnvironmentVariable("SendGrid:Sender:Email"),
                        Environment.GetEnvironmentVariable("SendGrid:Sender:Name")
                    );    

                sg.Recipient = new SendGridEmailUser(
                        Environment.GetEnvironmentVariable("SendGrid:Recipient:Email"),
                        Environment.GetEnvironmentVariable("SendGrid:Recipient:Name")
                    );           
                             
            });
      
        }
    }
}