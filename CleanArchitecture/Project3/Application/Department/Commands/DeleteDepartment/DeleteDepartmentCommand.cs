using MediatR;

namespace Application.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
