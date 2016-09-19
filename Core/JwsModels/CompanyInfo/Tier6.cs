using System.Data;

namespace Core.JwsModels.CompanyInfo
{
    public class Tier6 : BaseTier
    {
        public Tier6(IDataReader dr) : base(dr, "t6")
        {}
        public Tier6(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t6")
        { }
        public override int TierLevel => 6;
    }
}
