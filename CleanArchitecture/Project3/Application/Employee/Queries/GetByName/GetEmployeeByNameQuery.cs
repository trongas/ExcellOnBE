using Application.DTOs.Employee;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee.Queries.GetByName
{
    public class GetEmployeeByNameQuery : IRequest<ICollection<GetEmployeeDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
