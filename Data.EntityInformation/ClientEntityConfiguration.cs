using System.Collections.Generic;
using System.Linq;
using Data.EntityInformation.Models;

namespace Data.EntityInformation
{
    public class ClientEntityConfiguration
    {
        private static ClientEntityConfiguration _instance;
        public List<EntityConfiguration> EntityConfigurations { get; set; }

        public EntityConfiguration GetConfigurationByEntityCode(string entityCode)
        {
            return EntityConfigurations.Single(c => c.EntityCode == entityCode);
        }

        private ClientEntityConfiguration()
        {
            var appContext = new AppContext();
            EntityConfigurations = appContext.EntityConfigurations.ToList();
        }
        public static ClientEntityConfiguration Instance()
        {
            if (_instance == null)
            {
                _instance = new ClientEntityConfiguration();
            }

            return _instance;
        }
    }
}
