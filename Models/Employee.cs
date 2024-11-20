using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeASPNET.Models
{
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        //[Required, Display(Name = "Student Name")]
        public string? EmployeeName { get; set; }
        //[Required]
        public int Age { get; set; }
        //[Required]
        public DateTime DateOfJoining { get; set; }
        //[Required]
        public bool IsActive { get; set; }
        //[Required]
        public string? image { get; set; }
        public virtual IList<ProjectAssignment>? ProjectAssignments { get; set; }
    }
}
