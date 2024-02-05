using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_opendata.Data
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string UpdatedUser { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUsers User { get; set; }

        public virtual Staff_Document Staff_Document { get; set; }

    }
}
