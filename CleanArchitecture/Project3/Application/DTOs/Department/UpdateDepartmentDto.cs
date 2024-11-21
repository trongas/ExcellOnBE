using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Department
{
    public class UpdateDepartmentDto
    {
        [Required]
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
    }
}
