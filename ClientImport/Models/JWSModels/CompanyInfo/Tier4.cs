using System.Data;
using ClientImport.Infrastructure;
namespace ClientImport.Models.JWSModels.CompanyInfo
{
    public class Tier4 : BaseTier
    {
        public Tier4(IDataReader dr) : base(dr, "t4")
        {}
        public Tier4(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t4")
        { }
    }
}
