using System;
using System.Collections.Generic;
using AutoMapper;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Osceola
{
    public class ModelBuilder : BaseModelBuilder, IModelBuilder
    {
        public event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        public event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;
        public ModelBuilder()
        {
            CompanyNumber = Constants.Clients.OsceolaCompanyNumber;
            InitializeTiers();
            ConfigureMapper();


        }
        private void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {


                cfg.CreateMap<Record, JWSModels.Record>()
                    .ForMember(target => target.TierLevel              ,opts => opts.Ignore())
                    .ForMember(target => target.TierLevelId            ,opts => opts.Ignore())
                    .ForMember(target => target.TierName               ,opts => opts.Ignore())
                    .ForMember(target => target.UserLevel              ,opts => opts.Ignore())
                    .ForMember(target => target.IndexCode              ,opts => opts.Ignore())
                    .ForMember(target => target.NumberPayPeriods       ,opts => opts.Ignore())
                    .ForMember(target => target.PayRatePerPayPeriod    ,opts => opts.Ignore())
                    .ForMember(target => target.AnnualHours            ,opts => opts.Ignore())
                    .ForMember(target => target.AnnualPayRate          ,opts => opts.Ignore())
                    .ForMember(target => target.NameSuffix, opts => opts.ResolveUsing(c => c.NameSuffix))
                    .ForMember(target => target.JobClassCode, opts => opts.ResolveUsing(c => c.JobClassCode))
                    .ForMember(target => target.OccupationCode, opts => opts.Ignore())
                    .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                    .ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2))
                    .ForMember(target => target.UnionCode, opts => opts.Ignore())
                    .ForMember(target => target.PayRateType, opts => opts.UseValue("H"))
                     .AfterMap((src, target) =>
                     {


                         TierMapping tierMapping;//= new TierMapping(this, target);
                         if (!src.DivisionName.IsEmpty())
                         {
                             tierMapping = new TierMapping(this, target, src.DivisionName);
                             tierMapping.MapOrgLevel(Tiers3, 3, src.DivisionName.ToUpper(), src.DivisionName.ToUpper(),
                                 MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);

                         }


                     });


            });
            Mapper.AssertConfigurationIsValid();
        }

        public List<JWSModels.Record> GetJwsRecordsFromClientRecords(IEnumerable<IRecord<Record>> records)
        {
            var result = Mapper.Map<List<JWSModels.Record>>(records);
            return result;
        }
    }
}
