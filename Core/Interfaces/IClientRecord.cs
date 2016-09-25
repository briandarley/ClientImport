using System.Collections.Generic;
using System.Data;

namespace Core.Interfaces
{
    public interface IClientRecord
    {
        string JwsCompanyId { get; set; }

        IEnumerable<string> PropertyNames();

        IClientRecord GetRecord(string companyId, string record);
        IClientRecord GetRecord(string companyId, IDataReader dr);
    }
}
