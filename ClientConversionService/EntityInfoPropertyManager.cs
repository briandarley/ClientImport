using Data.EntityInformation.Models;
using Data.EntityInformation.Repositories;

namespace ClientConversionService
{
    public class EntityInfoPropertyManager
    {
        public void CreateEntityInfoForClient()
        {
            var repo = new EntityInfoRepository();
            repo.AddEntityConfiguration(new EntityConfiguration
            {
                EntityCode = Core.Constants.Entities.SarasotaCounty,
                CompanyNumber = "SC0100",
                Enabled = true,
                FileExtension = "xls",
                SourceFilePath = "Sarasota County"
            });
            repo.Save();


        }
    }
}
