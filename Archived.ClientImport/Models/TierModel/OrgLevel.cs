using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archived.ClientImport.Models.TierModel
{
    [Table("Organization")]
    public class OrgLevel
    {
        [Key, Column("Id", Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key, Column("Company_Number", Order = 1), MaxLength(30)]
        public string CompanyNumber { get; set; }
        [Key, Column("Level", Order = 2)]
        public int Level { get; set; }
        [Column("Name"), MaxLength(50)]
        public string Name { get; set; }
        [Column("Org_Number")]
        public string Number { get; set; }
        [Column("Tier_Id")]
        public string TierId{ get; set; }

    }
}
