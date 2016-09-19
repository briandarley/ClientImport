using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Conversion;
using Core.Infrastructure;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.Boca
{
    [EntityName(Core.Constants.Entities.Boca)]
    public class JwsConverter: BaseJwsConverter
    {
        public override IEnumerable<IClientRecord> GetClientRecords(string path)
        {
            var contents = File.ReadAllText(path);
            var records = contents.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var result = records.Select(SourceRecord.GetRecord);
            return result;
        }
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
            result.PhoneNumber = clientRecord.PhoneNumber;
            result.EmployeeId = clientRecord.EmployeeId;
            result.PayRatePerPayPeriod = clientRecord.PayRate;
            result.HoursWorkedPerDay = clientRecord.HoursWorkedPerDay;
            result.JobClassCode = clientRecord.JobClassCode;
            result.JobDescription = clientRecord.JobDescription;
            result.PayRate = clientRecord.PayRate;
            result.PayRateType = clientRecord.PayRateType;
            result.MaritalStatus = clientRecord.MaritalStatus;
            result.AddressLine1 = clientRecord.Address1;
            result.AddressLine2 = clientRecord.Address2;
            result.PayRatePerPayPeriod = clientRecord.PayRate;

            

            string departmentNumber = null;
            string level5 = null;
            string name = string.Empty;
            if (!clientRecord.DepartmentNumber.IsEmpty())
            {
                departmentNumber = clientRecord.DepartmentNumber;
                name = clientRecord.DepartmentName;
            }
            if (!clientRecord.Level5Number.IsEmpty())
            {
                level5 = clientRecord.Level5Number;
                name = clientRecord.Level5Name;
            }


            var args = new OrgLevelEventArgs(result)
            {
                CompanyId = result.Tier1CompanyId,
                DepartmentNumber = departmentNumber,
                O5Level = level5,
                Name =  name

            };

            
            OnOrgLevelEvent(args);
            result.TierName = args.CostCenterName;

            return result;
        }

       
    }
}
