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
        private readonly SendGridSettings _sendGridSettings;

        public OpenFiberFunction(
            IOptionsMonitor<Settings> settings,
            IOptionsMonitor<SendGridSettings> sendGridSettings
            )
        {
            _settings = settings.CurrentValue;
            _sendGridSettings = sendGridSettings.CurrentValue;
        }
        
        [FunctionName("OpenFiberFunction")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"Starting Open Fiber Function at: {DateTime.Now}");

            // Angle Sharp Config

            // Load default configuration and open context
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            // Parse the page by Url in a document
            var document = context.OpenAsync(_settings.OpenFiberUrl).Result;

            //Get elements from the page, and the coverage string
            var coverageElementParent = document.QuerySelector(_settings.AngleSharp_ParentElementIdentifier);
            var coverageStringElement = coverageElementParent.QuerySelector(_settings.AngleSharp_CoverageStringElementIdentifier);

            var coverageString = coverageStringElement.InnerHtml;

            //Define and assign the type of coverage
            CoverageEnum coverageEnum;

            if(coverageString == _settings.FTTHCoverageStringIdentifier){
                coverageEnum = CoverageEnum.FTTH;
            }
            else if(coverageString == _settings.FWACoverageStringIdentifier){
                coverageEnum = CoverageEnum.FWA;
            }
            else if(coverageString == _settings.NOCoverageStringIdentifier){
                coverageEnum = CoverageEnum.NoCoverage;
            }
            else{
                log.LogError($"The string {coverageString} is not expected, and it's not possible identify the coverage.");
            }

            
        }
    }
}
