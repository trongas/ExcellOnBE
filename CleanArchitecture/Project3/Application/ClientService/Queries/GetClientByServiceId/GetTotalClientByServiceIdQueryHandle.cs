using Domain.Abstraction;
using MediatR;

namespace Application.ClientService.Queries.GetClientByServiceId
{
    internal class GetTotalClientByServiceIdQueryHandle : IRequestHandler<GetTotalClientByServiceIdQuery, int>
    {
        public IUnitOfWork _unitOfWork { get; set; }

        public GetTotalClientByServiceIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> Handle(GetTotalClientByServiceIdQuery request, CancellationToken cancellationToken)
        {
            return _unitOfWork.ClientServices.GetTotalClientsByServiceId(request.ServiceId);
        }
    }
}
