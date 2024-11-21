using Application.DTOs.Service;
using Domain.Abstraction;
using MediatR;

namespace Application.Service.Commands.CreateService
{
    public class CreateServiceCommandHandle : IRequestHandler<CreateServiceCommand, CreateServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateServiceDto> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newService = new Domain.Entities.Service()
                {
                    ServiceName = request.ServiceDto.ServiceName,
                    Description = request.ServiceDto.Description,
                    TotalDay = request.ServiceDto.TotalDay,
                };

                bool isCreate = await _unitOfWork.Services.Add(newService);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    // Additional logic can be added here if needed
                    return request.ServiceDto;
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
