using Domain.Abstraction;
using MediatR;

namespace Application.Department.Commands.DeleteDepartment
{
    public class DeleteDepartmentCommandHandle : IRequestHandler<DeleteDepartmentCommand, bool>
    {
        public readonly IUnitOfWork _unitOfWork;
        public DeleteDepartmentCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var departmentDeleted = await _unitOfWork.Departments.GetById(request.Id);
                if (departmentDeleted != null)
                {
                    await _unitOfWork.Departments.Delete(departmentDeleted);
                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
