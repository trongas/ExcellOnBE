
using Application.DTOs.ClientService;
using MediatR;

namespace Application.ClientService.Commands.CreateClientService
{
    public class CreateClientServiceCommand : IRequest<CreateClientServiceDto>
    {
        public CreateClientServiceDto ClientServiceDto { get; set; }
    }
}
