using System.Collections.Generic;
using AutoMapper;
using ClientImport.Infrastructure;
using ClientImport.Infrastructure.Interfaces;

namespace ClientImport.Models.ClientModels.Client.Osceola
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
                    .ForMember(target => target.NameSuffix, opts => opts.ResolveUsing(c => c.NameSuffix))
                    .ForMember(target => target.JobClassCode, opts => opts.ResolveUsing(c => c.JobClassCode))
                    .ForMember(target => target.OccupationCode, opts=>opts.Ignore())
                    
                    .ForMember(target => target.AddressLine1, opts => opts.ResolveUsing(c => c.Address1))
                    .ForMember(target => target.AddressLine2, opts => opts.ResolveUsing(c => c.Address2))
                    .ForMember(target => target.UnionCode, opts => opts.Ignore())
                    .ForMember(target => target.GroupName, opts => opts.Ignore())
                    .ForMember(target => target.GroupNumber, opts => opts.Ignore())
                    .ForMember(target => target.PayRateType, opts => opts.UseValue("H"))
                    .ForMember(target => target.CompanyName, opts => opts.UseValue(Constants.Clients.OsceolaFullName))
                    .ForMember(target => target.CompanyNumber, opts => opts.UseValue(Constants.Clients.OsceolaCompanyNumber))
                    .ForMember(target => target.DepartmentNumber, opts => opts.Ignore())
                    .ForMember(target => target.DepartmentName, opts => opts.Ignore())
                    .ForMember(target => target.Level5Number, opts => opts.Ignore())
                    .ForMember(target => target.Level5Name, opts => opts.Ignore())
                    .ForMember(target => target.Level6Number, opts => opts.Ignore())
                    .ForMember(target => target.Level6Name, opts => opts.Ignore())
                    .ForMember(target => target.Level7Number, opts => opts.Ignore())
                    .ForMember(target => target.Level7Name, opts => opts.Ignore());
                

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
