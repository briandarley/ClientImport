using System;
using System.Collections.Generic;
using AutoMapper;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;
using Core.Interfaces;

namespace ClientImport.Models.ClientModels.Client.SarasotaCounty
{
    public partial class ModelBuilder : BaseModelBuilder
    {
        public event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        public event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;
        public ModelBuilder()
        {
            CompanyNumber = Constants.Clients.PinellasCompanyNumber;
            InitializeTiers();
            ConfigureMapper();


        }
        private void ConfigureMapper()
        {
            var config = Mapper.Configuration;
            Mapper.Initialize(cfg =>
            {


                cfg.CreateMap<Record, JWSModels.Record>()
                    .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                    .ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2))
                    .ForMember(target => target.DaysWorkedPerWeek, opts => opts.Ignore())
                    .ForMember(target => target.TierName, opts => opts.Ignore())
                    .ForMember(target => target.PayRatePerPayPeriod, opts => opts.Ignore());




            });
            config.AssertConfigurationIsValid();
        }

        public List<JWSModels.Record> GetJwsRecordsFromClientRecords(IEnumerable<IRecord<Record>> records)
        {
            var result = Mapper.Map<List<JWSModels.Record>>(records);
            return result;
        }
    }
}
