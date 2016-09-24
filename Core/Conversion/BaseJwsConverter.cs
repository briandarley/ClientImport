using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Core.Infrastructure;
using Core.Interfaces;
using Core.JwsModels;
using Core.JwsModels.CompanyInfo;
using Core.OrgMapping;
using Data.EntityInformation.Models;

namespace Core.Conversion
{
    public abstract class BaseJwsConverter
    {
        protected IClientRecord ClientRecord { get; set; }
        
        public EntityConfiguration EntityConfiguration { get; set; }


        protected BaseJwsConverter(IClientRecord clientRecord)
        {
            
            ClientRecord = clientRecord;
            MapOrgLevelHandler += JwsConverter_MapOrgLevelHandler;
        }

        
        private Organization _oranizartions;
        public List<IInvalidOrgLevel> MissingMappings { get; set; }
        public event EventHandler<OrgLevelEventArgs> MapOrgLevelHandler;
        

        
        public IEnumerable<IClientRecord> GetClientRecords(string path)
        {
            var expectedFileExtension = EntityConfiguration.FileExtension.ToUpper();
            switch (expectedFileExtension)
            {
                case "TXT":
                    return GetClientRecordsFromFlatFile(path);
                case "CSV":
                    return GetClientRecordsFromCsvFile(path);
                default:
                    throw new ArgumentException($"File Extension {expectedFileExtension} not expected. Please define proper conversion logic for file type");
            }
        }

        private IEnumerable<IClientRecord> GetClientRecordsFromFlatFile(string path)
        {
            var contents = File.ReadAllText(path);
            var records = contents.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var result = records.Select(c => ClientRecord.GetRecord(EntityConfiguration.CompanyNumber, c));
            return result;
        }
        private IEnumerable<IClientRecord> GetClientRecordsFromCsvFile(string path)
        {
            var root = Path.GetDirectoryName(path);

            var connectionString = string.Format(Constants.CsvConnectionString, root);
            using (var cn = new OleDbConnection(connectionString))
            {
                cn.Open();
                var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (schema == null) return null;

                var sheet1 = schema.Rows[0].Field<string>("TABLE_NAME");
                var cmd = cn.CreateCommand();
                cmd.CommandText = $"select * from [{sheet1}]";
                var dr = cmd.ExecuteReader();

                if (dr == null) return null;

                var result = new List<IClientRecord>();


                var rownum = 0;
                while (dr.Read())
                {
                    rownum++;

                    if (rownum == 1)
                    {
                        continue;
                    }
                    
                    result.Add(ClientRecord.GetRecord(EntityConfiguration.CompanyNumber, dr));

                }
                
                return result;
            }
        }
       

        public abstract IRecord GetJwsRecord(IClientRecord record);

      
        protected void OnOrgLevelEvent(OrgLevelEventArgs args)
        {
            MapOrgLevelHandler?.Invoke(this, args);
        }

        private void JwsConverter_MapOrgLevelHandler(object sender, OrgLevelEventArgs e)
        {
            var level = 0;
            var name = string.Empty;
            var number = string.Empty;
            if (e.DivisonNumber.IsEmpty() && e.DepartmentNumber.IsEmpty()) return;
            
            var organizationSet = false;
            if (_oranizartions == null)
            {
                _oranizartions = new Organization(e.CompanyId);
                var tiers = _oranizartions.Tiers;
                var count = _oranizartions.Tiers.Count;
            }

            //tiers.Where(c => c.Id == )

            if (!e.DivisonNumber.IsEmpty())
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.Number == e.DivisonNumber).ToList();
                var orgLevel4 = _oranizartions.Tiers.Where(c => c.TierLevel == 3 && c.Id.Contains(e.DivisonNumber)).ToList();
                level = 3;
                number = e.DivisonNumber;
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
                level = 4;
                name = e.Name;
                number = e.DepartmentNumber;
            }
            else if (!e.DepartmentNumber.IsEmpty() )
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.TierLevel == 4 && c.Id == e.DepartmentNumber.PadLeft(6,'0')).ToList();
                if (subOrgs.Any())
                {
                    var department = subOrgs.First();
                    e.Record.TierLevel = 4;
                    e.Record.TierLevelId = department.Id;
                    e.CostCenterName = department.Name;
                    organizationSet = true;
                }
                level = 4;
                number = e.DepartmentNumber;
            }
            if (!e.O5Level.IsEmpty())
            {
                var subOrgs = _oranizartions.Tiers.Where(c => c.Id.Contains(e.O5Level)).ToList();
                var orgLevel5 = _oranizartions.Tiers.Where(c => c.TierLevel == 5).ToList();

                level = 5;
            }




            if (!organizationSet)
            {
                var subOrg = _oranizartions.Tiers.Single(c => c.TierLevel == 2);
                e.Record.TierLevel = 2;
                e.Record.TierLevelId = subOrg.Id;

                var invalidOrgLevel = new InvalidOrgLevel
                {
                    Level = level,
                    Name = name,
                    Number = number
                };

                LogMissingMappings(invalidOrgLevel);
                
            }

            

        }

        private void LogMissingMappings(IInvalidOrgLevel record)
        {
            if (MissingMappings == null)
            {
                MissingMappings = new List<IInvalidOrgLevel>();
            }
            if (MissingMappings.Any(c => (InvalidOrgLevel)c == (InvalidOrgLevel)record)) return;

            MissingMappings.Add(record);
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
