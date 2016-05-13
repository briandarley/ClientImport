using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientImport.Infrastructure;
using ClientImport.Models.JWSModels;
using ClientImport.Models.JWSModels.CompanyInfo;

namespace ClientImport.Models.ClientModels
{
    public class TierMapping
    {
        private readonly IModelBuilder _builder;
        private readonly Record _record;
        private readonly string _organizationId;

        public TierMapping(IModelBuilder builder, Record record,string organizationId = "")
         {
            _builder = builder;
            _record = record;
            _organizationId = organizationId;
        }

        public void MapOrgLevel(
            IEnumerable<BaseTier> levelMappings, 
            int level, 
            string name, 
            string parentName,
            EventHandler<ClientLogEventArgs> missingMapping,
            EventHandler<ClientLogEventArgs> multMaping
            )
        {
            
            var levelList = levelMappings
                .Where(c => c.ParentTier.Name.ToUpper() == parentName.ToUpper())
                .Where(c => c.Name.ToUpper() == name.ToUpper())
                .GroupBy(c => new {c.Name, c.Id})
                .ToList();


            //if (!_organizationId.IsEmpty())
            //{
            //    levelList = levelList
            //            .Where(c => c.Number.Contains(_organizationId))
            //            .ToList();
            //}


            if (levelList.Any())
            {
                if (levelList.Count > 1)
                {
                    var args = new ClientLogEventArgs
                    {
                        Level = level,
                        Name = name,
                        ParentName = parentName
                    };
                    multMaping(_builder, args);

                    
                        
                }
                var organization = levelList.FirstOrDefault();
                
                _record.TierLevel = level;
                _record.TierLevelId = organization.Key.Id;
                _record.TierName = organization.Key.Name;
            }
            else if (!name.IsEmpty())
            {
                levelList = levelMappings
                       .Where(c => c.Name.ToUpper() == name.ToUpper())
                       .GroupBy(c => new { c.Name, c.Id })
                       .ToList();

                
                if (levelList.Count == 1)
                {
                    var organization = levelList.FirstOrDefault();
                    _record.TierLevel = level;
                    //_record.Tier1CompanyId = organization.Key.Id;
                    _record.TierName = organization.Key.Name;
                    _record.TierLevelId = organization.Key.Id;
                }
                else
                {
                    var args = new ClientLogEventArgs
                    {
                        Level = level,
                        Name = name,
                        ParentName = parentName
                    };

                    missingMapping(_builder, args);
                }

            }

        }
    }
}
