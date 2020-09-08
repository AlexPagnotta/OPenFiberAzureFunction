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
        public void Run([TimerTrigger("0 0 5 * * *")]TimerInfo myTimer, ILogger log)
        {
            var url = "https://openfiber.it/verifica-copertura/umbria/terni/fabro/?egon_id_for_civic=380100091821085&egon_id_for_address=38000648389&cityID=4805&street=via%20vittorio%20veneto&civic=31&cap=05015"
            
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            // This is where the HTTP request happens, returns <IDocument> that // we can query later
            var document = context.OpenAsync(url).Result;
            // Log the data to the console
            log.LogInformation(document.DocumentElement.OuterHtml);
        }
    }
}
