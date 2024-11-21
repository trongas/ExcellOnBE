using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Department
{
    public class GetDepartmentDto
    {

        public int Id { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentDescription { get; set; }
        public List<Domain.Entities.Employee> Employees { get; set; }
    }
}
