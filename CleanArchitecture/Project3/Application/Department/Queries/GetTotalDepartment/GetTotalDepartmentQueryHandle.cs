using Domain.Abstraction;
using Domain.Repositories;
using MediatR;

namespace Application.Department.Queries.GetTotalDepartment
{
    public class GetTotalDepartmentQueryHandle : IRequestHandler<GetTotalDepartmentQuery, int>
    {
        public readonly IUnitOfWork _unitOfWork;
        public GetTotalDepartmentQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(GetTotalDepartmentQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Departments.Count();
        }
    }
}
