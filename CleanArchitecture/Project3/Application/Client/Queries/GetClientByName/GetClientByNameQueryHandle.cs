using Application.DTOs.Client;
using Domain.Abstraction;
using Domain.Interfaces;
using MediatR;

namespace Application.Client.Queries.GetClientByName
{
    public class GetClientByNameQueryHandle : IRequestHandler<GetClientByNameQuery, ICollection<GetClientDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetClientByNameQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<GetClientDto>> Handle(GetClientByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clients = await _unitOfWork.Clients.GetClientsByName(request.FirstName, request.LastName);
                var clientsDto = new List<GetClientDto>();

                foreach (Domain.Entities.Client client in clients)
                {
                    var clientDto = new GetClientDto()
                    {
                        ClientId = client.ClientId,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Email = client.Email,
                        Dob = client.Dob,
                        Phone = client.Phone,
                    };
                    clientsDto.Add(clientDto);
                }

                return clientsDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query: " + ex);
                return null;
            }
        }
    }
}
