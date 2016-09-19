using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ClientImport.Infrastructure.Interfaces;
using ClientImport.Models.ClientModels.Client.BaptistHealth;
using ClientImport.Models.JWSModels.CompanyInfo;
using Core.Interfaces;

namespace ClientImport.Models.ClientModels
{
    public abstract class BaseModelBuilder
    {
        //public abstract event EventHandler MissingOrganizationMappingEncountered;
        



        public string CompanyNumber { get; set; }
        public string CompanyName { get; set; }

        public Organization Organization { get; set; }

        public List<Tier3> Tiers3 { get; set; }
        public List<Tier4> Tiers4 { get; set; }
        public List<Tier5> Tiers5 { get; set; }
        protected void InitializeTiers()
        {
            Organization = new Organization(CompanyNumber);

            var tier1 = Organization.Tiers
                .Where(c => c.GetType() == typeof(Tier1))
                .Cast<Tier1>().FirstOrDefault();

            if (tier1 == null) return;
            CompanyName = tier1.Name;
            CompanyNumber = tier1.Id;


            Tiers3 = Organization.Tiers
                .Where(c => c.GetType() == typeof (Tier3))
                .Cast<Tier3>().ToList();

            Tiers4 = Organization.Tiers
                .Where(c => c.GetType() == typeof(Tier4))
                .Cast<Tier4>().ToList();

            Tiers5 = Organization.Tiers
                .Where(c => c.GetType() == typeof(Tier5))
                .Cast<Tier5>().ToList();
        }


        

        public List<JWSModels.Record> GetJwsRecordsFromClientRecords<T>(IEnumerable<IRecord<T>> records) where T : new()
        {
            var result = Mapper.Map<List<JWSModels.Record>>(records);
            return result;
        }
    }
}
