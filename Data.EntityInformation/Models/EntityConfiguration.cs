using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.EntityInformation.Models
{
    [Table("ENTITY_CONFIGURATION")]
    public class EntityConfiguration
    {
        [Key, Column("ENTITY_CONFIGURATION_ID")]
        public int Id { get; set; }
        [Required, Column("ENTITY_CODE"), MaxLength(20)]
        public string EntityCode { get; set; }
        [Required, Column("FILE_EXTENSION"), MaxLength(10)]
        public string FileExtension { get; set; }
        [Required, Column("COMPANY_NUMBER"), MaxLength(30)]
        public string CompanyNumber { get; set; }
        [Column("SOURCE_FILE_PATH"), MaxLength(250)]
        public string SourceFilePath { get; set; }
        [Required, Column("ENABLED")]
        public bool Enabled { get; set; }
        [Column("SKIP_FIRST_LINE")]
        public bool SkipFirstLine { get; set; }

    }
}
