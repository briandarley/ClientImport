using System.Data;
using ClientImport.Infrastructure;
using ClientImport.Models.TierModel;

namespace ClientImport.Models.JWSModels.CompanyInfo
{
    public class Tier3 : BaseTier
    {
        public Tier3(IDataReader dr) : base(dr, "t3")
        {}
        public Tier3(BaseTier parentTier, IDataReader dr) : base(parentTier, dr, "t3")
        { }

        public Tier3(OrgLevel orgLevel) 
        {
            ParentTier = new Tier2();
            Id = orgLevel.TierId;
            Name = orgLevel.Name;
            Number = orgLevel.Number;
            WcLob = true;
        }
    }
}
