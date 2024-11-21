using Application.DTOs.Employee;
using MediatR;

namespace Application.Employee.Queries.GetAllEmployee
{
    public class GetAllEmployeeQuery : IRequest<ICollection<GetEmployeeDto>>
    {
    }
}
