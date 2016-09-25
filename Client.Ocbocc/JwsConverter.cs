using Core.Conversion;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.Ocbocc
{
    [EntityName(Core.Constants.Entities.OCBOCC)]
    public class JwsConverter : BaseJwsConverter
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
            //result.NameSuffix = clientRecord.NameSuffix;

            //result.SocialSecurityNumber = clientRecord.SocialSecurityNumber;
            result.Gender = clientRecord.Gender;
            result.MaritalStatus = clientRecord.MaritalStatus;

            result.DateOfBirth = clientRecord.DateOfBirth;
            result.AddressLine1 = clientRecord.Address1;
            //result.PhoneNumber = clientRecord.PhoneNumber;
            result.City = clientRecord.City;
            result.State = clientRecord.State;
            result.ZipCode = clientRecord.ZipCode;
            //result.HireDate = clientRecord.HireDate;
            result.EmployeeId = clientRecord.EmployeeId;
            //result.PayRate = clientRecord.PayRate;
            result.MaritalStatus = clientRecord.MaritalStatus;
            result.AddressLine1 = clientRecord.Address1;
            result.AddressLine2 = clientRecord.Address2;
            //result.DaysWorkedPerWeek = clientRecord.DaysWorkedPerWeek;
            //result.JobClassCode = clientRecord.JobClassCode;
            //result.JobDescription = clientRecord.JobDescription;
            

            

            var args = new OrgLevelEventArgs(record.JwsCompanyId, result)
            {
                CompanyId = result.Tier1CompanyId,
                //DivisonNumber = clientRecord.DivisionNumber,
            };
            

            OnOrgLevelEvent(args);
            result.TierName = args.CostCenterName;

            return result;
        }


    }
}
