using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeASPNET.Models
{
    public class Project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }
        //[Required, Display(Name = "Subject Name")]
        public string? ProjectName { get; set; }
        public virtual IList<ProjectAssignment> ProjectAssignments { get; set; }
    }
}
