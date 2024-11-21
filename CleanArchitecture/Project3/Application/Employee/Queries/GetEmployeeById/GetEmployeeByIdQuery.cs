using Application.DTOs.Employee;
using MediatR;

namespace Application.Employee.Queries.GetEmployeeById
{
    public class GetEmployeeByIdQuery : IRequest<GetEmployeeDto>
    {
        public int Id { get; set; }
    }
}
