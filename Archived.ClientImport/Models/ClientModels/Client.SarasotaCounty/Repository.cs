using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;

namespace Archived.ClientImport.Models.ClientModels.Client.SarasotaCounty
{
    public class Repository : BaseRepository<Record>
    {
        public sealed override string Tier2NullValue { get; set; }
        private ClientOrganizationInfos _multipleOrganizationMappings;
        private ClientOrganizationInfos _missingOrganizationMappings;
        public override ClientOrganizationInfos MultipleOrganizationMappings
        {
            get { return _multipleOrganizationMappings ?? (_multipleOrganizationMappings = new ClientOrganizationInfos()); }
            set { _multipleOrganizationMappings = value; }
        }

        public override ClientOrganizationInfos MissingOrganizationMappings
        {
            get
            {
                return _missingOrganizationMappings ?? (_missingOrganizationMappings = new ClientOrganizationInfos());
            }
            set { _missingOrganizationMappings = value; }
        }

        public override void OnMultipleOrganizationMappingEncountered(object sender, ClientLogEventArgs e)
        {
            if (MultipleOrganizationMappings == null)
            { MultipleOrganizationMappings = new ClientOrganizationInfos(); }

            if (MultipleOrganizationMappings.Any(c => c.Name == e.Name && c.ParentName == e.ParentName)) return;
            MultipleOrganizationMappings.Add(new ClientOrganizationInfo
            {
                Level = e.Level,
                Name = e.Name,
                ParentName = e.ParentName
            });

        }

        public override void OnMissingOrganizationMappingEncountered(object sender, ClientLogEventArgs e)
        {
            if (MissingOrganizationMappings == null)
            { MissingOrganizationMappings = new ClientOrganizationInfos(); }
            if (MissingOrganizationMappings.Any(c => c.Name == e.Name && c.ParentName == e.ParentName)) return;

            MissingOrganizationMappings.Add(new ClientOrganizationInfo
            {
                Level = e.Level,
                Name = e.Name,
                ParentName = e.ParentName
            });
        }




        public Repository() : base(Constants.Clients.SarasotaCounty, Constants.ConfigSarasotaCountyFileSource, Constants.ConfigSarasotaCountyFileExt)
        { }





        protected override List<JWSModels.Record> ConvertClientData(IEnumerable<IRecord<Record>> records)
        {
            
            var modelBuilder = new ModelBuilder();
            modelBuilder.MissingOrganizationMappingEncountered += OnMissingOrganizationMappingEncountered;
            modelBuilder.MultipleOrganizationMappingEncountered += OnMultipleOrganizationMappingEncountered;
            return modelBuilder.GetJwsRecordsFromClientRecords(records);

        }



        protected override IEnumerable<IRecord<Record>> ReadSourceFileRecords(string filePath)
        {
            try
            {
                var connectionString = string.Format(Constants.ExcelConnectionString, filePath);
                using (var cn = new OleDbConnection(connectionString))
                {
                    cn.Open();
                    var schema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    string sheet1 = schema.Rows[0].Field<string>("TABLE_NAME");
                    var cmd = cn.CreateCommand();
                    cmd.CommandText = $"select * from [{sheet1}]";
                    var dr = cmd.ExecuteReader();

                    var result = new List<IRecord<Record>>();


                    //var rownum = 0;
                    while (dr.Read())
                    {
                        //rownum++;

                        //if (rownum == 0)
                        //{
                        //    continue;
                        //}

                        result.Add(Record.GetRecord(dr));

                    }
                    return result;


                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Clients.SarasotaCounty, ex);

            }
            return null;
        }
    }
}
