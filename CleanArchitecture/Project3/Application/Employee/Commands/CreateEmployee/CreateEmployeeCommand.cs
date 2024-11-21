using Application.DTOs.Employee;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeDto>
    {
        public CreateEmployeeDto EmployeeDto { get; set; }

    }
}
