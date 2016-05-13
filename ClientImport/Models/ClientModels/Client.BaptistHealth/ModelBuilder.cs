using System;
using AutoMapper;
using ClientImport.Infrastructure;

namespace ClientImport.Models.ClientModels.Client.BaptistHealth
{
    public class ModelBuilder : BaseModelBuilder
    {

        public event EventHandler<ClientLogEventArgs> MissingOrganizationMappingEncountered;
        public event EventHandler<ClientLogEventArgs> MultipleOrganizationMappingEncountered;

        public ModelBuilder()
        {
            CompanyNumber = Constants.Clients.BaptistHealthCompanyNumber;

            InitializeTiers();
            ConfigureMapper();
        }







        private void ConfigureMapper()
        {
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
                    .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                    .ForMember(target => target.AddressLine2, opts => opts.Ignore())
                    .ForMember(target => target.DaysWorkedPerWeek, opts => opts.Ignore())
                    .ForMember(target => target.EmployeeId, opts => opts.Ignore())
                    .ForMember(target => target.JobClassCode, opts => opts.Ignore())
                    .ForMember(target => target.JobDescription, opts => opts.Ignore())
                    .ForMember(target => target.MaritalStatus, opts => opts.Ignore())
                    .ForMember(target => target.OccupationCode, opts => opts.Ignore())
                    .ForMember(target => target.PayRate, opts => opts.Ignore())
                    .ForMember(target => target.HoursWorkedPerDay, opts => opts.Ignore())
                    .ForMember(target => target.PayRateType, opts => opts.Ignore())
                    .ForMember(target => target.PhoneNumber, opts => opts.Ignore())
                    .ForMember(target => target.UnionCode, opts => opts.Ignore())
                    .ForMember(target => target.NameSuffix, opts => opts.Ignore());



            });
            Mapper.AssertConfigurationIsValid();
        }




    }
}
