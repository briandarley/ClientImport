using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.EntityInformation.Models;

namespace Data.EntityInformation.Repositories
{
    public class EntityInfoRepository
    {
        private AppContext _appContext;

        public EntityInfoRepository()
        {
            _appContext = new AppContext();
        }

        public EntityConfiguration GetEntityConfigurationByCode(string code)
        {
            var result = _appContext.EntityConfigurations.FirstOrDefault(c => c.EntityCode == code);
            return result;
        }

        public void AddEntityConfiguration(EntityConfiguration entityConfiguration)
        {
            _appContext.EntityConfigurations.Add(entityConfiguration);
        }

        public void Save()
        {
            _appContext.SaveChanges();
        }


    }
}
