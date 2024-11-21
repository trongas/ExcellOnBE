using MediatR;

namespace Application.Client.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int ClientId { get; set; }
    }
}
