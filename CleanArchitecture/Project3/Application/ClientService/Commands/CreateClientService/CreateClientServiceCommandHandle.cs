
using Application.DTOs.ClientService;
using Domain.Abstraction;
using MediatR;

namespace Application.ClientService.Commands.CreateClientService
{
    public class CreateClientServiceCommandHandle : IRequestHandler<CreateClientServiceCommand, CreateClientServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientServiceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateClientServiceDto> Handle(CreateClientServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newClientService = new Domain.Entities.ClientService()
                {
                    Status = request.ClientServiceDto.Status,
                    ExpiredDay = request.ClientServiceDto.ExpiredDay,
                    ServiceId = request.ClientServiceDto.ServiceId,
                    ClientId = request.ClientServiceDto.ClientId,
                    StartDay = request.ClientServiceDto.StartDay
                };

                bool isCreate = await _unitOfWork.ClientServices.Add(newClientService);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    // Additional logic can be added here if needed
                    return request.ClientServiceDto;
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
