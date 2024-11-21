using Domain.Abstraction;
using MediatR;

namespace Application.Employee.Queries.GetTotalEmployee
{
    public class GetTotalEmployeeQueryHandle : IRequestHandler<GetTotalEmployeeQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTotalEmployeeQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> Handle(GetTotalEmployeeQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.Employees.Count();
        }
    }
}
