using System.Data;
using ClientImport.Infrastructure;
namespace ClientImport.Models.JWSModels.CompanyInfo
{
    public class Tier3 : BaseTier
    {
        public Tier3(IDataReader dr) : base(dr, "t3")
        {}
        public Tier3(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t3")
        { }
    }
}
