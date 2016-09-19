using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;
using Core.Interfaces;

namespace ClientImport.Models.ClientModels.Client.BaptistHealth
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


        public Repository() : base(Constants.Clients.BaptistHealth, Constants.ConfigBaptistHealthFileSource, Constants.ConfigBaptistHealthFileExt)
        {
            Tier2NullValue = "000108";
        }


       

        private IEnumerable<IEnumerable<IRecord<Record>>> GetAllRecords()
        {
            foreach (var fileInfo in FileSystemFiles)
            {
                _logger.LogGetRecordsForGivenPath(Constants.Clients.BaptistHealth, fileInfo.FullName);
                IEnumerable<IRecord<Record>> records = ReadSourceFileRecords(fileInfo.FullName);
                yield return records;

            }
        }

        //protected override List<JWSModels.Record> ConvertClientData(IEnumerable<IRecord<Record>> records)
        //{
        //    throw new NotImplementedException();
        //}

        protected override List<JWSModels.Record> ConvertClientData(IEnumerable<IRecord<Record>> records)
        {

            var modelBuilder = new ModelBuilder();
            var modelRecords = modelBuilder.GetJwsRecordsFromClientRecords(records);


            foreach (var record in modelRecords)
            {
                //if (record.TierLevel == 0)
                //{
                //    record.TierLevel = 2;
                //}
                record.Tier1CompanyId = "000108";//Constants.Clients.BaptistHealthCompanyNumber.PadLeft(6, '0');
                //record.TierLevelId = "000108";//Constants.Clients.BaptistHealthCompanyNumber.PadLeft(6, '0');

                //if (string.IsNullOrEmpty(record.ZipCode))
                //{
                //    record.ZipCode = "00000";
                //}
                //if (record.ZipCode.Contains("-"))
                //{
                //    record.ZipCode = Regex.Match(record.ZipCode, @"^\d+").Value;
                //}
                //if (string.IsNullOrEmpty(record.AddressLine1))
                //{
                //    record.AddressLine1 = "x";
                //}
                //if (string.IsNullOrEmpty(record.City))
                //{
                //    record.City = "x";
                //}
                //if (string.IsNullOrEmpty(record.State))
                //{
                //    record.State = "FL";
                //}
                //if (record.DateOfBirth == DateTime.MinValue)
                //{
                //    record.DateOfBirth = new DateTime(1950, 1, 1);
                //}
                //if (string.IsNullOrEmpty(record.MaritalStatus))
                //{
                //    record.MaritalStatus = "Unknown";
                //}
                //if (string.IsNullOrEmpty(record.SocialSecurityNumber))
                //{
                //    record.SocialSecurityNumber = new string('0', 9);
                //}
                //record.SocialSecurityNumber = record.SocialSecurityNumber.PadLeft(9, '0');
            }

            return modelRecords;

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
                _logger.LogError(Constants.Clients.BaptistHealth, ex);

            }
            return null;
        }
    }
}
