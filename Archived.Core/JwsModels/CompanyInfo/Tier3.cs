using System.Data;
using Archived.Core.Interfaces;

namespace Archived.Core.JwsModels.CompanyInfo
{
    public class Tier3 : BaseTier
    {
        public Tier3(IDataReader dr) : base(dr, "t3")
        {}
        public Tier3(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t3")
        { }

        public Tier3(IOrgLevel orgLevel) 
        {
            ParentTier = new Tier2();
            Id = orgLevel.TierId;
            Name = orgLevel.Name;
            Number = orgLevel.Number;
            WcLob = true;
        }
        public override int TierLevel => 3;
    }
}
