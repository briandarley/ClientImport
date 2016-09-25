using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archived.Core.JwsComapnyInfo
{
    [Table("fh_company")]
public    class ComapnyInfo
    {
        [Key, Column("company_id",Order = 0),MaxLength(6)]
        public string CompanyId { get; set; }
        [Key, Column("company_code", Order = 1), MaxLength(6)]
        public string CompanyCode { get; set; }
        [Key, Column("company_group", Order = 2), MaxLength(1)]
        public string CompanyGroup { get; set; }
        [Key, Column("tier_type", Order = 3), MaxLength(1)]
        public string TierType { get; set; }
        [Column("company_name"), MaxLength(40)]
        public string CompanyName { get; set; }
        [Column("tier1_company_id"), MaxLength(6)]
        public string Tier1CompanyId { get; set; }
        [Column("tier2_company_id"), MaxLength(6)]
        public string Tier2CompanyId { get; set; }
        [Column("tier3_company_id"), MaxLength(6)]
        public string Tier3CompanyId { get; set; }
        [Column("tier4_company_id"), MaxLength(6)]
        public string Tier4CompanyId { get; set; }
        [Column("tier5_company_id"), MaxLength(6)]
        public string Tier5CompanyId { get; set; }
        [Column("tier6_company_id"), MaxLength(6)]
        public string Tier6CompanyId { get; set; }
        [Column("tier1_name"), MaxLength(30)]
        public string Tier1Name { get; set; }
        [Column("tier2_name"), MaxLength(30)]
        public string Tier2Name { get; set; }
        [Column("tier3_name"), MaxLength(30)]
        public string Tier3Name { get; set; }
        [Column("tier4_name"), MaxLength(30)]
        public string Tier4Name { get; set; }
        [Column("tier5_name"), MaxLength(30)]
        public string Tier5Name { get; set; }
        [Column("tier6_name"), MaxLength(30)]
        public string Tier6Name { get; set; }
        [Column("address1"), MaxLength(40)]
        public string Address1 { get; set; }
        [Column("address2"), MaxLength(40)]
        public string Address2 { get; set; }
        [Column("city"), MaxLength(25)]
        public string City { get; set; }

        [Column("state"), MaxLength(2)]
        public string State { get; set; }

        [Column("zip_code"), MaxLength(10)]
        public string ZipCode { get; set; }
        [Column("cpcpnum")]
        public int CompanyNumber { get; set; }
        [Column("wc_lob_yn")]
        public string FileType { get; set; }
    }
}
