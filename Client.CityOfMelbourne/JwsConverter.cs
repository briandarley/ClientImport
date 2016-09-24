using Core;
using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.CityOfMelbourne
{
    [EntityName(Constants.Entities.CityOfMelbourne)]
    public class JwsConverter : BaseJwsConverter
    {
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
            result.PhoneNumber = clientRecord.PhoneNumber;
            result.EmployeeId = clientRecord.EmployeeId;
            //result.PayRatePerPayPeriod = clientRecord.PayRatePerPeriod;
            result.HoursWorkedPerDay = clientRecord.HoursWorkedPerDay;
            result.JobClassCode = clientRecord.JobClassCode;
            result.JobDescription = clientRecord.JobDescription;
            result.PayRate = clientRecord.PayRate ?? 0;
            result.PayRateType = clientRecord.PayRateType;
            result.MaritalStatus = clientRecord.MaritalStatus;
            result.AddressLine1 = clientRecord.Address1;
            result.AddressLine2 = clientRecord.Address2;
            result.AnnualHours = clientRecord.AnnualHours;
            result.UnionCode = clientRecord.UnionCode;
            result.OccupationCode = clientRecord.OccupationCode;
            string departmentNumber = null;


            if (!clientRecord.DepartmentNumber.IsEmpty())
            {
                departmentNumber = clientRecord.DepartmentNumber;
            }
            


            var args = new OrgLevelEventArgs(record.JwsCompanyId, result)
            {
                CompanyId = result.Tier1CompanyId,
                DepartmentNumber = departmentNumber,
            };


            OnOrgLevelEvent(args);
            result.TierName = args.CostCenterName;

            return result;
        }



    }
}
