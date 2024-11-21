using Application.DTOs.ServiceCharges;
using Domain.Abstraction;
using MediatR;

namespace Application.ServiceCharges.Commands.CreateServiceCharges
{
    public class CreateServiceChargesCommandHandle : IRequestHandler<CreateServiceChargesCommand, CreateServiceChargesDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateServiceChargesCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateServiceChargesDto> Handle(CreateServiceChargesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Assuming ServiceCharges entity has properties like ServiceName, Amount, etc.
                var newServiceCharges = new Domain.Entities.ServiceCharges()
                {
                    ServiceChargesName = request.ServiceChargesDto.ServiceChargesName,
                    Price = request.ServiceChargesDto.Price,
                    ServiceChargesDescription = request.ServiceChargesDto.ServiceChargesDescription,
                    ServiceId = request.ServiceChargesDto.ServiceId


                };

                Console.WriteLine(newServiceCharges);

                bool isCreate = await _unitOfWork.ServicesCharges.Add(newServiceCharges);
                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    return request.ServiceChargesDto;
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
