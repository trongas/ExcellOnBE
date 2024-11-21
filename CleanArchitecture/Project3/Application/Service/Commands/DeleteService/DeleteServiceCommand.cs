using MediatR;

namespace Application.Service.Commands.DeleteService
{
    public class DeleteServiceCommand : IRequest<bool>
    { 
        public int ServiceId { get; set; }
    }
}
