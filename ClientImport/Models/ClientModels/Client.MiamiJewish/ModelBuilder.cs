using System.Collections.Generic;
using AutoMapper;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.MiamiJewish
{
    public class ModelBuilder
    {
        public ModelBuilder()
        {
            ConfigureMapper();


        }
        private void ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                

                cfg.CreateMap<Record, JWSModels.Record>()
                    .ForMember(target => target.DaysWorkedPerWeek, opts => opts.Ignore())
                    //.ForMember(target => target.DivisionNumber, opts => opts.ResolveUsing(c=>c.DivisionNumber))
                    .ForMember(target => target.GroupName, opts => opts.Ignore())
                    .ForMember(target => target.GroupNumber, opts => opts.Ignore())
                    .ForMember(target => target.Level5Name, opts => opts.Ignore())
                    .ForMember(target => target.Level5Number, opts => opts.Ignore())
                    .ForMember(target => target.Level6Name, opts => opts.Ignore())
                    .ForMember(target => target.Level6Number, opts => opts.Ignore())
                    .ForMember(target => target.Level7Name, opts => opts.Ignore())
                    .ForMember(target => target.Level7Number, opts => opts.Ignore())
                    .ForMember(target => target.OccupationCode, opts => opts.Ignore())
                    .ForMember(target => target.UnionCode, opts => opts.Ignore())
                    .ForMember(target => target.NameSuffix, opts => opts.Ignore())
                    .ForMember(target => target.HoursWorkedPerDay, opts => opts.Ignore())
                    
                    .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                    .ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2))
                    .ForMember(target => target.City, opts => opts.ResolveUsing(c => c.City))
                    .ForMember(target => target.CompanyName, opts => opts.UseValue(Constants.Clients.BocaFullName))
                    .ForMember(target => target.CompanyNumber,opts => opts.UseValue(Constants.Clients.BocaCompanyNumber))
                    .ForMember(target => target.DateOfBirth, opts => opts.ResolveUsing(c => c.DateOfBirth))
                    .ForMember(target => target.DepartmentName, opts => opts.ResolveUsing(c => c.DepartmentName))
                    .ForMember(target => target.DepartmentNumber, opts => opts.ResolveUsing(c => c.DepartmentNumber))
                    .ForMember(target => target.DivisionName, opts => opts.ResolveUsing(c => c.DivisionName))
                    .ForMember(target => target.EmployeeId, opts => opts.ResolveUsing(c => c.EmployeeId))
                    .ForMember(target => target.FirstName, opts => opts.ResolveUsing(c => c.FirstName))
                    .ForMember(target => target.Gender, opts => opts.ResolveUsing(c => c.Gender))
                    .ForMember(target => target.HireDate, opts => opts.ResolveUsing(c => c.HireDate))
                    .ForMember(target => target.JobClassCode, opts => opts.ResolveUsing(c => c.JobClassCode))
                    .ForMember(target => target.JobDescription, opts => opts.ResolveUsing(c => c.JobDescription))
                    .ForMember(target => target.LastName, opts => opts.ResolveUsing(c => c.LastName))
                    .ForMember(target => target.MaritalStatus, opts => opts.ResolveUsing(c => c.MaritalStatus))
                    .ForMember(target => target.PayRate, opts => opts.ResolveUsing(c => c.PayRate))
                    .ForMember(target => target.PayRateType, opts => opts.ResolveUsing(c => c.PayRateType))
                    .ForMember(target => target.PhoneNumber, opts => opts.ResolveUsing(c => c.PhoneNumber))
                    .ForMember(target => target.SocialSecurityNumber,opts => opts.ResolveUsing(c => c.SocialSecurityNumber))
                    .ForMember(target => target.State, opts => opts.ResolveUsing(c => c.State))
                    .ForMember(target => target.ZipCode, opts => opts.ResolveUsing(c => c.ZipCode));


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
