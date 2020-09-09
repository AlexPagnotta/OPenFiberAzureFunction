using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Html.Parser;

namespace AlexPagnotta.Function
{
    public static class OpenFiberFunction
    {
        [FunctionName("OpenFiberFunction")]
        public static void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var url = Environment.GetEnvironmentVariable("OpenFiberUrl");
            var FTTHCoverageString = Environment.GetEnvironmentVariable("FTTHCoverageString");
            var FWACoverageString = Environment.GetEnvironmentVariable("FWACoverageString");
            var NOCoverageString = Environment.GetEnvironmentVariable("NOCoverageString");
            
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = context.OpenAsync(url).Result;

            var connectionStatusElementParent = document.QuerySelector(".search-by-address.covered-type");
            var connectionStatusString = connectionStatusElementParent.QuerySelector("strong");

            var coverageString = connectionStatusString.InnerHtml;

            // Log the data to the console
            log.LogInformation(coverageString);

            var emailString = "";

            if(coverageString == FTTHCoverageString){
                emailString = "Finally FTTH!";
            }
            else if(coverageString == FWACoverageString){
                emailString = "FWA, Nothing Changed";
            }
            else if(coverageString == NOCoverageString){
                emailString = "What, No Coverage Now?";
            }
            else{
                //Exception;
            }

            
        }
    }
}
