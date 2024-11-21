using Application.DTOs.Client;
using Domain.Abstraction;
using MediatR;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommandHandle : IRequestHandler<CreateClientCommand, CreateClientDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateClientCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newClient = new Domain.Entities.Client()
                {
                    FirstName = request.ClientDto.FirstName,
                    LastName = request.ClientDto.LastName,
                    Email = request.ClientDto.Email,
                    Phone = request.ClientDto.Phone,
                    Dob = request.ClientDto.Dob,
                };

                Console.WriteLine(newClient);

                bool isCreate = await _unitOfWork.Clients.Add(newClient);
                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    return request.ClientDto;
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
