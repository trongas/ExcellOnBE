using Application.DTOs.Client;
using MediatR;

namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<CreateClientDto>
    {
        public CreateClientDto ClientDto { get; set; }
    }
}
