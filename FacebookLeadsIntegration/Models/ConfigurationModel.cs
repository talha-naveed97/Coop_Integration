using Data.Entities;
using Models.Enums;

namespace Models
{
    public class ConfigurationModel
    {
        public ConfigurationModel(ConfigurationEntity configurationEntity)
        {
            //Map Entity to model
        }
        public string ConfigurationName { get; set; }
        public ConfigurationTypes Type { get; set; }
        public string ObjectId { get; set; }
        public DateTime LastExecution { get; set; }
        public string Schedule { get; set; } // Probably a cron schedule that will be evaluated on runtime
        public bool Enabled { get; set; }
        public List<string> RecipientEmails { get; set; }
    }

}
