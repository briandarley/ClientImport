using System;
using System.Collections.Generic;
using AutoMapper;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;

namespace Archived.ClientImport.Models.ClientModels.Client.Boca
{
    public partial class ModelBuilder : BaseModelBuilder, IModelBuilder
    {
        public event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        public event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;

        public ModelBuilder()
        {
            CompanyNumber = Constants.Clients.BocaCompanyNumber;

            InitializeTiers();
            ConfigureMapper();
        }



        private IMapper mapper;
        private void ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
           {


                cfg.CreateMap<Record, JWSModels.Record>()
                .ForMember(target => target.DaysWorkedPerWeek, opts => opts.Ignore())
                .ForMember(target => target.OccupationCode, opts => opts.Ignore())
                .ForMember(target => target.UnionCode, opts => opts.Ignore())
                .ForMember(target => target.NumberPayPeriods, opts => opts.Ignore())
                .ForMember(target => target.AnnualHours, opts => opts.Ignore())
                .ForMember(target => target.AnnualPayRate, opts => opts.Ignore())
                .ForMember(target => target.TierLevel, opts => opts.Ignore())
                .ForMember(target => target.TierLevelId, opts => opts.Ignore())
                .ForMember(target => target.TierName, opts => opts.Ignore())
                .ForMember(target => target.UserLevel, opts => opts.Ignore())
                .ForMember(target => target.IndexCode, opts => opts.Ignore())
                .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                .ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2))
                .ForMember(target => target.PayRatePerPayPeriod, opts => opts.ResolveUsing(c => c.PayRate))
                .AfterMap((src, target) =>
                {


                    var tierMapping = new TierMapping(this, target);
                    if (!src.Level5Name.IsEmpty())
                    {
                        tierMapping.MapOrgLevel(Tiers5, 5, src.Level5Name.ToUpper(), src.DepartmentName.ToUpper(),
                            MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);
                    }
                    if (!src.DepartmentName.IsEmpty())
                    {
                        tierMapping = new TierMapping(this, target, src.DepartmentNumber);
                        tierMapping.MapOrgLevel(Tiers4, 4, src.DepartmentName.ToUpper(), src.DivisionName.ToUpper(),
                            MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);
                    }
                    if (target.TierLevel == 0)
                    {
                        target.TierLevel = 2;
                        target.TierLevelId = "109".PadLeft(6, '0');

                    }




                });


            });
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();

        }

        public List<JWSModels.Record> GetJwsRecordsFromClientRecords(IEnumerable<IRecord<Record>> records)
        {

            var result = mapper.Map<List<JWSModels.Record>>(records);
            return result;
        }

    }
}
