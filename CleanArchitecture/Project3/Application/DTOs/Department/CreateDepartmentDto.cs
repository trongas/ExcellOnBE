using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTOs.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string DepartmentDescription { get; set; }
    }
}
