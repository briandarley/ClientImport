using System.Data;
using Archived.ClientImport.Infrastructure;
namespace Archived.ClientImport.Models.JWSModels.CompanyInfo
{
    public class Tier5 : BaseTier
    {
        public Tier5(IDataReader dr) : base(dr, "t5")
        {}
        public Tier5(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t5")
        { }
    }
}
