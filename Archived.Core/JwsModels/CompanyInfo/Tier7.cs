using System.Data;

namespace Archived.Core.JwsModels.CompanyInfo
{
    public class Tier7 : BaseTier
    {
        public Tier7(IDataReader dr) : base(dr, "t7")
        {}

        public override int TierLevel => 7;
    }
}
