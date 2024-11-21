using Application.DTOs.Employee;
using MediatR;

namespace Application.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public int EmployeeId { get; set; }
    }
}
