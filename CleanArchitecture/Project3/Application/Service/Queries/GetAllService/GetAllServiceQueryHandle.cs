using Application.DTOs.Service;
using Domain.Abstraction;
using MediatR;

namespace Application.Service.Queries.GetAllService
{
    public class GetAllServiceQueryHandle : IRequestHandler<GetAllServiceQuery, ICollection<GetServiceDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllServiceQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<GetServiceDto>> Handle(GetAllServiceQuery request, CancellationToken cancellationToken)
        {
            var services = await _unitOfWork.Services.All();
            var servicesDto = new List<GetServiceDto>();

            foreach (Domain.Entities.Service s in services)
            {
                var serviceDto = new GetServiceDto()
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.ServiceName,
                    Description = s.Description,
                    TotalDay = s.TotalDay,

                };
                servicesDto.Add(serviceDto);
            }

            return servicesDto;
        }
    }
}
