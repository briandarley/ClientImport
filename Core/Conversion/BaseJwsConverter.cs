using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Infrastructure;
using Core.Interfaces;
using Core.JwsModels;
using Core.JwsModels.CompanyInfo;
using Core.OrgMapping;

namespace Core.Conversion
{
    public abstract class BaseJwsConverter
    {
        //private List<OrgLevel> _orgLevels;
        private List<IRecord> _missingMappings;
        private Organization _oranizartions;

        public event EventHandler<OrgLevelEventArgs> MapOrgLevelHandler;
        public abstract IEnumerable<IClientRecord> GetClientRecords(string path);
        public abstract IRecord GetJwsRecord(IClientRecord record);

        protected BaseJwsConverter()
        {

            MapOrgLevelHandler += JwsConverter_MapOrgLevelHandler;
        }
        protected void OnOrgLevelEvent(OrgLevelEventArgs args)
        {
            MapOrgLevelHandler?.Invoke(this, args);
        }

        private void JwsConverter_MapOrgLevelHandler(object sender, OrgLevelEventArgs e)
        {
            if (e.DivisonNumber.IsEmpty() && e.DepartmentNumber.IsEmpty()) return;

            var organizationSet = false;
            if (_oranizartions == null)
            {
                _oranizartions = new Organization(Constants.CompanyNumbers.Boca);
                var tiers = _oranizartions.Tiers;
                var count = _oranizartions.Tiers.Count;
            }

            //tiers.Where(c => c.Id == )

            if (!e.DivisonNumber.IsEmpty())
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.Number == e.DivisonNumber).ToList();
                var orgLevel4 = _oranizartions.Tiers.Where(c => c.TierLevel == 3 && c.Id.Contains(e.DivisonNumber)).ToList();
            }
            if (!e.DepartmentNumber.IsEmpty() && !e.Name.IsEmpty())
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.TierLevel == 4 && c.Name.ToUpper() == e.Name.ToUpper()).ToList();
                if (subOrgs.Any())
                {
                    var department = subOrgs.First();
                    e.Record.TierLevel = 4;
                    e.Record.TierLevelId = department.Id;
                    e.CostCenterName = department.Name;
                    organizationSet = true;
                }
            }
            else if (!e.DepartmentNumber.IsEmpty() )
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.TierLevel == 4).ToList();
                if (subOrgs.Count != 0 )
                {
                    Console.WriteLine("");
                }
            }
            if (!e.O5Level.IsEmpty())
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.Id.Contains(e.O5Level)).ToList();
                var orgLevel5 = _oranizartions.Tiers.Where(c => c.TierLevel == 5).ToList();
            }




            if (!organizationSet)
            {
                var subOrg = _oranizartions.Tiers.Single(c => c.TierLevel == 2);
                e.Record.TierLevel = 2;
                e.Record.TierLevelId = subOrg.Id;
                
            }










            //if (_orgLevels == null)
            //{
            //    var appContext = new Infrastructure.AppContext();
            //    _orgLevels = appContext.OrgLevels.Where(c => c.CompanyNumber == e.CompanyId).ToList();
            //}

                //Func<OrgLevel, bool> predicate = o =>
                //{
                //if (!e.DivisonNumber.HasValue && !e.DepartmentNumber.HasValue) return false;
                //level = e.DivisonNumber.HasValue ? 3 : 4;

                //return o.Level == level.Value;
                //};


                //var currentLevel = e.DivisonNumber.HasValue
                //? _orgLevels.FirstOrDefault(predicate)
                //: _orgLevels.FirstOrDefault(c => c.Level == 4 && c.Number == e.DepartmentNumber.Value.ToString());



                //if (currentLevel == null)
                //{
                //LogMissingMappings(e.Record);
                //return;
                //}


                //e.Record.Tier1CompanyId = currentLevel.CompanyNumber;
                //e.Record.TierName = currentLevel.Name;
                //e.Record.TierLevel = currentLevel.Level;

        }

        private void LogMissingMappings(IRecord record)
        {
            if (_missingMappings == null)
            {
                _missingMappings = new List<IRecord>();
            }
            _missingMappings.Add(record);
        }



        public string CreateOuputFile(IEnumerable<IRecord> list)
        {
            var clientName = "test";
            var repo = new Repository { Records = list.Cast<Record>().ToList() };


            var basePath = $@"{Constants.BaseDestinationPath}\{clientName}\";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            var fileName = "OUT_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff");
            var outputPath = Path.Combine(basePath, fileName + ".xlsx");
            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            repo.WriteRecordsToExcelFile(outputPath);
            return outputPath;
        }
    }
}
