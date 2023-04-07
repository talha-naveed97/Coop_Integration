using Logic.LeadsAd;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace FunctionApp
{
    public class LeadsRetrivalFunction
    {
        private readonly ILeadsAdService _leadsAdService;

        public LeadsRetrivalFunction(ILeadsAdService leadsAdService)
        {
            _leadsAdService = leadsAdService ?? throw new ArgumentNullException(nameof(leadsAdService));
        }

        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            _leadsAdService.ProcessLeads();
        }
    }
}
