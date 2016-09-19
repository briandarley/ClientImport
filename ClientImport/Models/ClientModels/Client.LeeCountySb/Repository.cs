using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;
using Core.Interfaces;

namespace ClientImport.Models.ClientModels.Client.LeeCountySb
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

        public override  ClientOrganizationInfos MissingOrganizationMappings
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
        
        public Repository() : base(Constants.Clients.LeeCountySchoolBoard, Constants.ConfigLeeCountySbFileSource, Constants.ConfigLeeCountySbFileExt)
        { }


        private IEnumerable<IEnumerable<IRecord<Record>>> GetAllRecords()
        {
            foreach (var fileInfo in FileSystemFiles)
            {
                _logger.LogGetRecordsForGivenPath(Constants.Clients.LeeCountySchoolBoard, fileInfo.FullName);
                IEnumerable<IRecord<Record>> records = ReadSourceFileRecords(fileInfo.FullName);
                yield return records;

            }
        }

        


        protected override IEnumerable<IRecord<Record>> ReadSourceFileRecords(string filePath)
        {
            try
            {
                var contents = File.ReadAllText(filePath);
                var records = contents.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                var result = records.Select(Record.GetRecord).ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Constants.Clients.LeeCountySchoolBoard, ex);

            }
            return null;
        }


        protected override List<JWSModels.Record> ConvertClientData(IEnumerable<IRecord<Record>> records)
        {

            var modelBuilder = new ModelBuilder();
            modelBuilder.MissingOrganizationMappingEncountered += OnMissingOrganizationMappingEncountered;
            modelBuilder.MultipleOrganizationMappingEncountered += OnMultipleOrganizationMappingEncountered;
            return modelBuilder.GetJwsRecordsFromClientRecords(records);

        }

    }






}
