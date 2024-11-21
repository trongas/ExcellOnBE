using Application.DTOs.Service;
using Domain.Abstraction;
using MediatR;

namespace Application.Service.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandle : IRequestHandler<GetServiceByIdQuery, GetServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetServiceByIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetServiceDto> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await _unitOfWork.Services.GetById(request.ServiceId);

                if (service == null)
                {
                    return null;
                }

                GetServiceDto serviceDto = new GetServiceDto
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    Description = service.Description,
                    TotalDay = service.TotalDay,
                };

                return serviceDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
