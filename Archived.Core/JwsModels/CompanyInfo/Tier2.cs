using System.Data;

namespace Archived.Core.JwsModels.CompanyInfo
{
    public class Tier2 : BaseTier
    {
        public Tier2(IDataReader dr) : base(dr, "t2")
        { }
        public Tier2(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t2")
        { }

        public Tier2()
        {
            Name = string.Empty;

        }
        public override int TierLevel => 2;
    }
}
