using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_opendata.Data
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string DocumentFormat { get; set; }
        public string DocumentSize { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; } = false;

        public int RepositoryId { get; set; }
        [ForeignKey("RepositoryId")]
        public virtual Repository Repository { get; set; }

        public virtual Staff_Document Staff_Document { get; set; }
    }
}
