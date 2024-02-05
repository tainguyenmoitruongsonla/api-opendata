using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_opendata.Data
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DepartmentName { get; set; }

        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }  
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Staff> Staff { get; set; }
    }
}
