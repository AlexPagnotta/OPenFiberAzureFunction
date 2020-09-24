using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using AngleSharp;
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
        public void Run([TimerTrigger("30 7 * * 1-5")]TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"Starting Open Fiber Function at: {DateTime.Now}");

            //Get the coverage from the page
            var coverageEnum = GetCoverageFromPageUrl(_settings.OpenFiberAddressUrl);

            //Build email content
            var emailContent = BuildEmail(_settings.OpenFiberAddress, coverageEnum); 

            //Send the email with SendGrid

            var emailSender = new EmailSender(
                _sendGridSettings.API_KEY, 
                _sendGridSettings.Sender, 
                _sendGridSettings.Recipient
                );

            emailSender.SendEmail(emailContent.subject, emailContent.content).GetAwaiter().GetResult();

        }

        
        public CoverageEnum GetCoverageFromPageUrl(string pageUrl) {

            // Angle Sharp Config

            // Load default configuration and open context
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);

            // Parse the page by Url in a document
            var document = context.OpenAsync(pageUrl).Result;

            //Get elements from the page, and the coverage string
            var coverageElementParent = document.QuerySelector(_settings.ParentElementIdentifier);
            var coverageStringElement = coverageElementParent.QuerySelector(_settings.CoverageStringElementIdentifier);

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
                throw new Exception($"The string {coverageString} is not expected, and it's not possible to identify the coverage.");
            }       

            return coverageEnum;
        }


        public (string subject, string content) BuildEmail(string openFiberAddress, CoverageEnum coverageEnum){

            //Define subjec and content based on the coverage enum
            var subject = "";
            var content = "";

            switch (coverageEnum)
            {
                case CoverageEnum.FTTH:
                    subject = "Sei connesso in FTTH!";
                    content = $"Il tuo indirizzo {openFiberAddress} è connesso in FTTH!";
                    break;
                case CoverageEnum.FWA:
                    subject = "Sei connesso in FWA.";
                    content = $"Il tuo indirizzo {openFiberAddress} è connesso in FWA!";
                    break;
                case CoverageEnum.NoCoverage:
                    subject = "Mi dispiace, non sei connesso...";
                    content = $"Il tuo indirizzo {openFiberAddress} non è ancora coperto!";
                    break;
            }

            return (subject, content);

        }
    }
}
