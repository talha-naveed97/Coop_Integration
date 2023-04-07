using API.Facebook;
using Logic.Configuration;
using Logic.Email;
using Logic.Mappers;
using Microsoft.Extensions.Logging;
using Models;
using Models.Enums;
using System.Text;
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
        private readonly IEmailService _emailService;

        public LeadsAdService(ILogger<LeadsAdService> logger,
            IFacebookApiService facebookApiService,
            IConfigurationService configurationService,
            IMapper<LeadsAdModel, InteractionModel> leadsAdToInteractionMapper,
            IEmailService emailService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _facebookApiService = facebookApiService ?? throw new ArgumentNullException(nameof(facebookApiService));
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _leadsAdToInteractionMapper = leadsAdToInteractionMapper ?? throw new ArgumentNullException(nameof(leadsAdToInteractionMapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        }

        public void ProcessLeads()
        {
            // Get Configurations
            var configurations = _configurationService.GetEnabledConfigurations();
            // Loop over all enabled configurations
            foreach (var configuration in configurations)
            {
                // Check if time to execute for specific config
                if (!IsTimeToExecute(configuration.LastExecution, configuration.Schedule))
                    continue;

                // Get Leads
                var leads = (configuration.Type == ConfigurationTypes.AdGroup) ? _facebookApiService.GetLeadsAdByAdGroup(configuration.ObjectId) :
                    _facebookApiService.GetLeadsAdByForm(configuration.ObjectId);

                if (!leads.Any())
                    continue;

                //Map to interactions
                var interactions = new List<InteractionModel>();
                foreach (var lead in leads)
                    interactions.Add(_leadsAdToInteractionMapper.Map(lead));

                // Send file as email attachment or we can put on a ftp server or anywhere the file can be accessed | Can be decided
                var fileName = (configuration.Type == ConfigurationTypes.AdGroup) ? $"AdGroupLeads_{configuration.ObjectId}_{DateTime.Now.ToString()}" :
                    $"Form_{configuration.ObjectId}_{DateTime.Now.ToString()}";

                _emailService.SendEmailWithAttachment(configuration.RecipientEmails, new MemoryStream(Encoding.ASCII.GetBytes(GetXml(interactions))), fileName, System.Net.Mime.MediaTypeNames.Text.Xml);
                
            }
        }

        private bool IsTimeToExecute(DateTime lastExecution, string schedule)
        {
            // Evaluate the scedule based on the last execution time and return a bool to say if it is time to excute or not
            return true;
        }

        private string GetXml(IEnumerable<InteractionModel> interactions)
        {
            // Get XML
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
