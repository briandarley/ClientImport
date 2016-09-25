using System;
using System.Collections.Generic;
using AutoMapper;
using Archived.ClientImport.Infrastructure;
using Archived.ClientImport.Infrastructure.Interfaces;

namespace Archived.ClientImport.Models.ClientModels.Client.LeeCountySb
{
    public class ModelBuilder : BaseModelBuilder, IModelBuilder
    {
        public event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        public event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;

        public ModelBuilder()
        {
            CompanyNumber = Constants.Clients.LeeCountySchoolBoardCompanyNumber;

            InitializeTiers();
            ConfigureMapper();
        }

        private IMapper mapper;
        private void ConfigureMapper()
        {
            //var config = Mapper.Configuration;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Record, JWSModels.Record>().ForMember(target => target.TierLevel, opts => opts.Ignore()).ForMember(target => target.TierLevelId, opts => opts.Ignore()).ForMember(target => target.TierName, opts => opts.Ignore()).ForMember(target => target.UserLevel, opts => opts.Ignore()).ForMember(target => target.IndexCode, opts => opts.Ignore()).ForMember(target => target.NumberPayPeriods, opts => opts.Ignore()).ForMember(target => target.PayRatePerPayPeriod, opts => opts.Ignore()).ForMember(target => target.AnnualHours, opts => opts.Ignore()).ForMember(target => target.AnnualPayRate, opts => opts.Ignore()).ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1)).ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2)).ForMember(target => target.City, opts => opts.ResolveUsing(c => c.City)).ForMember(target => target.DateOfBirth, opts => opts.ResolveUsing(c => c.DateOfBirth)).ForMember(target => target.DaysWorkedPerWeek, opts => opts.Ignore()).ForMember(target => target.EmployeeId, opts => opts.Ignore()).ForMember(target => target.FirstName, opts => opts.ResolveUsing(c => c.FirstName)).ForMember(target => target.Gender, opts => opts.ResolveUsing(c => c.Gender)).ForMember(target => target.HireDate, opts => opts.ResolveUsing(c => c.HireDate)).ForMember(target => target.JobClassCode, opts => opts.Ignore()).ForMember(target => target.JobDescription, opts => opts.Ignore()).ForMember(target => target.LastName, opts => opts.ResolveUsing(c => c.LastName)).ForMember(target => target.OccupationCode, opts => opts.Ignore()).ForMember(target => target.PayRate, opts => opts.Ignore()).ForMember(target => target.HoursWorkedPerDay, opts => opts.Ignore()).ForMember(target => target.PayRateType, opts => opts.Ignore()).ForMember(target => target.PhoneNumber, opts => opts.Ignore()).ForMember(target => target.SocialSecurityNumber, opts => opts.ResolveUsing(c => c.SocialSecurityNumber)).ForMember(target => target.State, opts => opts.ResolveUsing(c => c.State)).ForMember(target => target.UnionCode, opts => opts.Ignore()).ForMember(target => target.NameSuffix, opts => opts.Ignore()).ForMember(target => target.ZipCode, opts => opts.ResolveUsing(c => c.ZipCode)).AfterMap((src, target) =>
                {
                    TierMapping tierMapping; //= new TierMapping(this, target);

                    if (!src.DepartmentName.IsEmpty())
                    {
                        tierMapping = new TierMapping(this, target, src.DepartmentName);
                        tierMapping.MapOrgLevel(Tiers4, 4, src.DepartmentName.ToUpper(), src.DepartmentName.ToUpper(), MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);
                    }

                    if (!src.DivisionName.IsEmpty())
                    {
                        tierMapping = new TierMapping(this, target, src.DivisionName);
                        tierMapping.MapOrgLevel(Tiers3, 3, src.DivisionName.ToUpper(), src.DivisionName.ToUpper(), MissingOrganizationMappingEncountered, MultipleOrganizationMappingEncountered);
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
