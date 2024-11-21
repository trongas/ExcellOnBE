using Application.DTOs.Department;
using Domain.Abstraction;
using MediatR;

namespace Application.Department.Queries.GetAllDepartment
{
    public class GetAllDepartmentQueryHandle : IRequestHandler<GetAllDepartmentQuery, List<GetDepartmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDepartmentQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<GetDepartmentDto>> Handle(GetAllDepartmentQuery request, CancellationToken cancellationToken)
        {
            var departments = await _unitOfWork.Departments.All();
            if (departments == null)
            {
                return null;
            }
            List<GetDepartmentDto> departmentDtos = new List<GetDepartmentDto>();
            foreach (var department in departments)
            {
                if (department != null)
                {
                    departmentDtos.Add(new GetDepartmentDto()
                    {
                        Id = department.Id,
                        DepartmentDescription = department.DepartmentDescription,
                        DepartmentName = department.DepartmentName
                    });
                }
            }
            return departmentDtos;
        }
    }
}
