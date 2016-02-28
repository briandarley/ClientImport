using System.Data;
using ClientImport.Infrastructure;
namespace ClientImport.Models.JWSModels.CompanyInfo
{
    public class Tier2 : BaseTier
    {
        public Tier2(IDataReader dr) : base(dr, "t2")
        { }
        public Tier2(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t2")
        { }


    }
}
