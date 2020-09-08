using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AlexPagnotta.Function
{
    public static class OpenFiberFunction
    {
        [FunctionName("OpenFiberFunction")]
        public static void Run([TimerTrigger("0 0 5 * * *")]TimerInfo myTimer, ILogger log)
        {
            //URL https://openfiber.it/verifica-copertura/umbria/terni/fabro/?egon_id_for_civic=380100091821085&egon_id_for_address=38000648389&cityID=4805&street=via%20vittorio%20veneto&civic=31&cap=05015
            
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
