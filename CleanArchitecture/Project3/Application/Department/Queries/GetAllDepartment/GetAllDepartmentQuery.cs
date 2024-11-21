using Application.DTOs.Department;
using MediatR;

namespace Application.Department.Queries.GetAllDepartment
{
    public class GetAllDepartmentQuery : IRequest<List<GetDepartmentDto>>
    {
    }
}
