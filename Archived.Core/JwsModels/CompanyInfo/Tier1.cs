using System.Data;

namespace Archived.Core.JwsModels.CompanyInfo
{
    public class Tier1 : BaseTier
    {
        public Tier1(IDataReader dr):base(dr, "t1")
        { }
        public override int TierLevel => 1;


    }
}
