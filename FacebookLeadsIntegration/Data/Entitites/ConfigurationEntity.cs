
namespace Data.Entities
{
    public class ConfigurationEntity
    {
        public string ConfigurationName { get; set; }
        public int Type { get; set; }
        public string ObjectId { get; set; }
        public DateTime LastExecution { get; set; }
        public string Schedule { get; set; }
        public bool Enabled { get; set; }
        public List<string> RecipientEmails { get; set; }
    }

}
