using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using AngleSharp;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Options;

namespace AlexPagnotta.Function
{
    public class OpenFiberFunction
    {
        private readonly Settings _settings;

        public OpenFiberFunction(IOptionsMonitor<Settings> settings)
        {
            _settings = settings.CurrentValue;
        }
        
        [FunctionName("OpenFiberFunction")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"Starting Open Fiber Function at: {DateTime.Now}");

            // Getting variables from Json 

            // Url of the open fiber coverage page
            var url = Environment.GetEnvironmentVariable("OpenFiberUrl");

            // This strings identifies if you are covered and by wich technology
            var FTTHCoverageString = Environment.GetEnvironmentVariable("FTTHCoverageStringIdentifier");
            var FWACoverageString = Environment.GetEnvironmentVariable("FWACoverageStringIdentifier");
            var NOCoverageString = Environment.GetEnvironmentVariable("NOCoverageStringIdentifier");
            
            // This strings identifies the elements selectors on the page, needed to obtain the string that indicates the coverage
            var parentElementIdentifier = Environment.GetEnvironmentVariable("AngleSharp_ParentElementIdentifier");
            var coverageStringElementIdentifier = Environment.GetEnvironmentVariable("AngleSharp_CoverageStringElementIdentifier");

            // Angle Sharp Config

            // Load default configuration and open context
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            // Parse the page by Url in a document
            var document = context.OpenAsync(url).Result;

            //Get elements from the page, and the coverage string
            var coverageElementParent = document.QuerySelector(parentElementIdentifier);
            var coverageStringElement = coverageElementParent.QuerySelector(coverageStringElementIdentifier);

            var coverageString = coverageStringElement.InnerHtml;

            //Define and assign the type of coverage
            CoverageEnum coverageEnum;

            if(coverageString == FTTHCoverageString){
                coverageEnum = CoverageEnum.FTTH;
            }
            else if(coverageString == FWACoverageString){
                coverageEnum = CoverageEnum.FWA;
            }
            else if(coverageString == NOCoverageString){
                coverageEnum = CoverageEnum.NoCoverage;
            }
            else{
                log.LogError($"The string {coverageString} is not expected, and it's not possible identify the coverage.");
            }

            
        }
    }
}
