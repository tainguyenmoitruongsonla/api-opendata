using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_opendata.Data
{
    public class Repository
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string RepositoryName { get; set; }
        public string RepositoryType { get; set; }
        public string RepositoryCapacity { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string UpdatedUser { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Document> Document { get; set; }
    }
}
