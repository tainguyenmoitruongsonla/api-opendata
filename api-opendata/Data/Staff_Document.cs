using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_opendata.Data
{
    public class Staff_Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StaffId { get; set; }
        public int DocumentId { get; set; }

        [ForeignKey("StaffId")]
        public virtual ICollection<Staff> Staff { get; set; }

        [ForeignKey("DocumentId")]
        public virtual ICollection<Document> Document { get; set; }
    }
}
