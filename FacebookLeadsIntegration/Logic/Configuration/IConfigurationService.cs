using Models;

namespace Logic.Configuration
{
    public interface IConfigurationService
    {
        IEnumerable<ConfigurationModel> GetEnabledConfigurations();
    }
}
