using Application.DTOs.ClientService;
using Domain.Abstraction;
using MediatR;

namespace Application.ClientService.Queries.GetAllClientService
{
    public class GetAllClientServiceQueryHandle : IRequestHandler<GetAllClientServiceQuery, ICollection<GetClientServiceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllClientServiceQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetClientServiceDto>> Handle(GetAllClientServiceQuery request, CancellationToken cancellationToken)
        {
            var clientServices = await _unitOfWork.ClientServices.All();
            var clientServicesDto = new List<GetClientServiceDto>();

            foreach (Domain.Entities.ClientService cs in clientServices)
            {
                var clientServiceDto = new GetClientServiceDto()
                {
                    Status = cs.Status,
                    ExpiredDay = cs.ExpiredDay,
                    ServiceId = cs.ServiceId,
                    ClientId = cs.ClientId,
                    StartDay = cs.StartDay
                };
                clientServicesDto.Add(clientServiceDto);
            }

            return clientServicesDto;
        }
    }
}
