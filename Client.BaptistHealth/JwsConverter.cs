using Core.Conversion;
using Core.Interfaces;
using Core.JwsModels;
using Core.OrgMapping;

namespace Client.BaptistHealth
{
    [EntityName(Core.Constants.Entities.BaptistHealth)]
    public class JwsConverter: BaseJwsConverter
    {
        
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
            
            var args = new OrgLevelEventArgs(record.JwsCompanyId,result)
            {
                CompanyId = result.Tier1CompanyId,
            };

            
            OnOrgLevelEvent(args);


            return result;
        }

        public JwsConverter(IClientRecord clientRecord):base(clientRecord)
        {}
    }
}
