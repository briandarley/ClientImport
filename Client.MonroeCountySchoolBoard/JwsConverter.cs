using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.MonroeCountySchoolBoard
{
    [EntityName(Core.Constants.Entities.MonroeCountySchoolBoard)]
    public class JwsConverter: BaseJwsConverter
    {
        protected override bool SkipFirstLine { get; set; }
        public JwsConverter(IClientRecord clientRecord) : base(clientRecord)
        { }
        
        public override IRecord GetJwsRecord(IClientRecord record)
        {
            var clientRecord = record as SourceRecord;
            if (clientRecord == null)
            {
                return null;
            }

            
            var result = new Record();
            
            result.Tier1CompanyId = clientRecord.JwsCompanyId;
            result.LastName = clientRecord.LastName;
            result.FirstName = clientRecord.FirstName;
            result.MiddleInitial = clientRecord.MiddleInitial;
            result.SocialSecurityNumber = clientRecord.SocialSecurityNumber;
            result.Gender = clientRecord.Gender;
            result.DateOfBirth = clientRecord.DateOfBirth;
            result.AddressLine1 = clientRecord.Address1;
            result.City = clientRecord.City;
            result.State = clientRecord.State;
            result.ZipCode = clientRecord.ZipCode;
            result.HireDate = clientRecord.HireDate;
            result.EmployeeId = clientRecord.EmployeeId;
            //result.PayRatePerPayPeriod = clientRecord.PayRateType;
            result.PayRate = clientRecord.PayRate??0;
            result.MaritalStatus = clientRecord.MaritalStatus;
            result.AddressLine1 = clientRecord.Address1;
            result.AddressLine2 = clientRecord.Address2;
            //result.PayRatePerPayPeriod = clientRecord.;

            
            
            var args = new OrgLevelEventArgs(record.JwsCompanyId,result)
            {
                CompanyId = result.Tier1CompanyId,
                DivisionName = clientRecord.DivisionName,
                DivisonNumber = clientRecord.DivisionNumber,
                
            };

            
            OnOrgLevelEvent(args);
            result.TierName = args.CostCenterName;

            return result;
        }

       
    }
}
