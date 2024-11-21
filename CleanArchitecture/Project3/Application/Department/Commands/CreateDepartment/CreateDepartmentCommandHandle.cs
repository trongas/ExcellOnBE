using Application.DTOs.Department;
using Domain.Abstraction;
using MediatR;

namespace Application.Department.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandle : IRequestHandler<CreateDepartmentCommand, CreateDepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDepartmentCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateDepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newDepartment = new Domain.Entities.Department
                {
                    DepartmentName = request.CreateDepartment.DepartmentName,
                    DepartmentDescription = request.CreateDepartment.DepartmentDescription,
                };
                bool isCreate = await _unitOfWork.Departments.Add(newDepartment);
                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    return request.CreateDepartment;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
