using Application.DTOs.Department;
using MediatR;

namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<CreateDepartmentDto>
    {
        public CreateDepartmentDto CreateDepartment { get; set; }
    }
}
