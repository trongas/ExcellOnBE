using Application.DTOs.Department;
using MediatR;

namespace Application.Department.Commands.UpdateDepartment
{
    public class UpdateDepartmentCommand : IRequest<UpdateDepartmentDto>
    {
        public UpdateDepartmentDto UpdateDepartmentDto { get; set; }
    }
}
