using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IClientRecord
    {
        string JwsCompanyId { get; set; }

        IEnumerable<string> PropertyNames();
    }
}
