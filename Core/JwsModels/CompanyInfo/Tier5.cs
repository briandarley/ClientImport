using System.Data;

namespace Core.JwsModels.CompanyInfo
{
    public class Tier5 : BaseTier
    {
        public Tier5(IDataReader dr) : base(dr, "t5")
        {}
        public Tier5(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t5")
        { }
        public override int TierLevel => 5;
    }
}
