using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeASPNET.Models.Vm
{
    public class EmployeeVm
    {
        public EmployeeVm()
        {
            this.ProjectAssignments = new List<ProjectAssignment>();
        }
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string? EmployeeName { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 100, ErrorMessage = "Age must be between 1 and 100.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Date of Joining is required.")]
        public DateTime DateOfJoining { get; set; }
       
        [NotMapped]
        public IFormFile? imageFile { get; set; }
        public string? image { get; set; }

        public bool IsActive { get; set; }
        public virtual List<ProjectAssignment>? ProjectAssignments { get; set; }
    }
}
