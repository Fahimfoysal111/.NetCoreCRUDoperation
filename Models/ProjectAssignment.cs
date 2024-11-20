using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeASPNET.Models
{
    public class ProjectAssignment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }
        //[Required, Display(Name = "Student Name")]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }
}
