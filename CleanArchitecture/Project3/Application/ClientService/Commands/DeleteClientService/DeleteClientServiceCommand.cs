using MediatR;

namespace Application.ClientService.Commands.DeleteClientService
{
    public class DeleteClientServiceCommand : IRequest<bool>
    {
        public int ClientServiceId { get; set; }
    }
}
