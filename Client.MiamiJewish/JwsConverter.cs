using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.MiamiJewish
{
    [EntityName(Core.Constants.Entities.MiamiJewish)]
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
            //JwsCompanyId = Core.Constants.CompanyNumbers.BaptistHealth
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

            

            string departmentNumber = null;
            string divisionName = clientRecord.DivisionName;
            string name = string.Empty;
            if (!clientRecord.DepartmentNumber.IsEmpty())
            {
                departmentNumber = clientRecord.DepartmentNumber;
                name = clientRecord.DepartmentName;
            }
            
            


            var args = new OrgLevelEventArgs(record.JwsCompanyId,result)
            {
                CompanyId = result.Tier1CompanyId,
                DivisionName = divisionName,
                DepartmentNumber = departmentNumber,
                
                Name =  name

            };

            
            OnOrgLevelEvent(args);
            result.TierName = args.CostCenterName;

            return result;
        }

       
    }
}
