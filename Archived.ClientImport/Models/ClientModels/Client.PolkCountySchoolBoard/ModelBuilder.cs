using System;
using System.Collections.Generic;
using AutoMapper;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;

namespace Archived.ClientImport.Models.ClientModels.Client.PolkCountySchoolBoard
{
    public partial class ModelBuilder : BaseModelBuilder, IModelBuilder
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
                    .ForMember(target => target.TierLevel, opts => opts.Ignore())
                    .ForMember(target => target.TierLevelId, opts => opts.Ignore())
                    .ForMember(target => target.TierName, opts => opts.Ignore())
                    .ForMember(target => target.UserLevel, opts => opts.Ignore())
                    .ForMember(target => target.IndexCode, opts => opts.Ignore())
                    .ForMember(target => target.NumberPayPeriods, opts => opts.Ignore())
                    .ForMember(target => target.PayRatePerPayPeriod, opts => opts.Ignore())
                    .ForMember(target => target.AnnualHours, opts => opts.Ignore())
                    .ForMember(target => target.AnnualPayRate, opts => opts.Ignore())
                    .ForMember(target => target.DaysWorkedPerWeek, opts => opts.Ignore())
                    .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                    .ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2))
                      .AfterMap((src, target) =>
                      {


                          TierMapping tierMapping;//= new TierMapping(this, target);
                          if (!src.DivisionName.IsEmpty())
                          {
                              tierMapping = new TierMapping(this, target, src.DivisionName);
                              tierMapping.MapOrgLevel(Tiers3, 3, src.DivisionName.ToUpper(), src.DivisionName.ToUpper(),
                                  MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);

                          }
                          if (!src.DepartmentName.IsEmpty())
                          {
                              tierMapping = new TierMapping(this, target, src.DepartmentName.Trim());
                              tierMapping.MapOrgLevel(Tiers4, 4, src.DepartmentName.Trim().ToUpper(), src.DivisionName.Trim().ToUpper(),
                                  MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);

                          }


                      });






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
