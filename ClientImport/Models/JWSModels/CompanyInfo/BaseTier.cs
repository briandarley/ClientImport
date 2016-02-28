using System.Data;
using ClientImport.Infrastructure;
namespace ClientImport.Models.JWSModels.CompanyInfo
{
    public abstract class BaseTier
    {
        public string  Prefix{ get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public bool WcLob { get; set; }
        public BaseTier ParentTier { get; set; }
        protected BaseTier(IDataReader dr, string prefix)
        {
            Id = dr.GetString($"{prefix}_id");
            Name = dr.GetString($"{prefix}_name");
            Number = dr.GetString($"{prefix}_cpcpnum");
            WcLob = dr.GetString($"{prefix}_wc_lob_yn") == "W";
        }
        protected BaseTier(BaseTier parentTier, IDataReader dr, string prefix)
        {
            ParentTier = parentTier;
            Id = dr.GetString($"{prefix}_id");
            Name = dr.GetString($"{prefix}_name");
            Number = dr.GetString($"{prefix}_cpcpnum");
            WcLob = dr.GetString($"{prefix}_wc_lob_yn") == "W";
        }

        
    }
}
