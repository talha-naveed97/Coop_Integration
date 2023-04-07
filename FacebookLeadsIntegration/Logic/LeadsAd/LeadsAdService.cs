using API.Facebook;
using Logic.Configuration;
using Logic.Mappers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Models;
using Models.Enums;
using System.Xml;
using System.Xml.Serialization;

namespace Logic.LeadsAd
{
    internal class LeadsAdService : ILeadsAdService
    {
        private readonly ILogger<LeadsAdService> _logger;
        private readonly IFacebookApiService _facebookApiService;
        private readonly IConfigurationService _configurationService;
        private readonly IMapper<LeadsAdModel, InteractionModel> _leadsAdToInteractionMapper;

        public LeadsAdService(ILogger<LeadsAdService> logger,
            IFacebookApiService facebookApiService,
            IConfigurationService configurationService,
            IMapper<LeadsAdModel, InteractionModel> leadsAdToInteractionMapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _facebookApiService = facebookApiService ?? throw new ArgumentNullException(nameof(facebookApiService));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _leadsAdToInteractionMapper = leadsAdToInteractionMapper ?? throw new ArgumentNullException(nameof(leadsAdToInteractionMapper));
        }

        public void ProcessLeads()
        {
            // Get Configurations
            var configurations = _configurationService.GetEnabledConfigurations();
            // Loop over all enabled configurations
            foreach (var configuration in configurations)
            {
                if (!IsTimeToExecute(configuration.LastExecution, configuration.Schedule))
                    continue;

                var leads = (configuration.Type == ConfigurationTypes.AdGroup) ? _facebookApiService.GetLeadsAdByAdGroup(configuration.ObjectId) :
                    _facebookApiService.GetLeadsAdByForm(configuration.ObjectId);

                if (!leads.Any())
                    continue;

                var interactions = new List<InteractionModel>();
                foreach (var lead in leads)
                    interactions.Add(_leadsAdToInteractionMapper.Map(lead));

                
            }
        }

        private bool IsTimeToExecute(DateTime lastExecution, string schedule)
        {
            // Evaluate the scedule based on the last execution time and return a bool to say if it is time to excute or not
            return true;
        }

        private string GetXml(IEnumerable<InteractionModel> interactions)
        {
            //var serializer = new XmlSerializer(interactions.GetType());
            //using (var writer = new StreamWriter("interactions.xml"))
            //{
            //    serializer.Serialize(writer, interactions);
            //    writer.ToString();
            //}

            var xml = "";
            using (StringWriter sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    XmlSerializer xsSubmit = new XmlSerializer(interactions.GetType());
                    xsSubmit.Serialize(writer, interactions);
                    xml = sww.ToString();
                }
            }
            return xml;
        }
    }
}
