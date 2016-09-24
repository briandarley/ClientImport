using Core.Conversion;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.PinellasCounty
{
    [EntityName(Core.Constants.Entities.PinellasCounty)]
    public class JwsConverter: BaseJwsConverter
    {
        public JwsConverter(IClientRecord clientRecord) : base(clientRecord)
        { }
        
        public override IRecord GetJwsRecord(IClientRecord record)
        {
            var clientRecord = record as SourceRecord;
            if (clientRecord == null)
            {
                return null;
                //throw new ArgumentException("Client Record is Empty or Invalid type");
            }

            //var propertyNames = clientRecord.PropertyNames();
            //foreach (var propertyName in propertyNames)
            //{
            //    Console.WriteLine(propertyName);
            //}
            //result.TierLevel = ??
            //result.TierLevelId = ??
            //result.TierName = ??
            //result.UserLevel = ??
            //result.IndexCode = ??
            //result.NumberPayPeriods = ??
            var result = new Record();
            result.Tier1CompanyId = clientRecord.JwsCompanyId;
            result.LastName = clientRecord.LastName;
            result.FirstName = clientRecord.FirstName;
            result.MiddleInitial = clientRecord.MiddleInitial;
            result.SocialSecurityNumber = clientRecord.SocialSecurityNumber;
            result.Gender = clientRecord.Gender;
            result.DateOfBirth = clientRecord.DateOfBirth;
            result.MaritalStatus = clientRecord.MaritalStatus;
            result.AddressLine1 = clientRecord.Address1;
            result.AddressLine2 = clientRecord.Address2;
            result.City = clientRecord.City;
            result.State = clientRecord.State;
            result.ZipCode = clientRecord.ZipCode;
            result.EmployeeId = clientRecord.EmployeeId;
            result.JobDescription = clientRecord.JobTitle;
            result.JobClassCode = clientRecord.JobClassCode;
            result.DaysWorkedPerWeek = (decimal)clientRecord.DaysWorkedPerWeek;
            result.HoursWorkedPerDay = (decimal)clientRecord.HoursWorkedPerDay;
            //result.DivisionNumber = clientRecord.DivisionNumber;
            result.PayRateType = clientRecord.PayRateType;
            result.PayRate = clientRecord.PayRate;
            result.HireDate = clientRecord.HireDate;

            //var companyNumber = result.Tier1CompanyId;
            //InitializeOrganizationList(companyNumber);

            var args = new OrgLevelEventArgs(record.JwsCompanyId,result)
            {
                CompanyId = result.Tier1CompanyId,
                DivisonNumber = clientRecord.DivisionNumber
            };

            
            OnOrgLevelEvent(args);


            return result;
        }

       
    }
}
