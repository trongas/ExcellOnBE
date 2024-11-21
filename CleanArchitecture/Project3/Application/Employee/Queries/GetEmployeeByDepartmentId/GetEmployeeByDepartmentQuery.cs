using Application.DTOs.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Queries.GetEmployeeByDepartmentId
{
    public class GetEmployeeByDepartmentQuery : IRequest<List<GetEmployeeDto>>
    {
        public int Id { get; set; }
    }
}
