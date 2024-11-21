
using Application.DTOs.Service;
using MediatR;

namespace Application.Service.Commands.UpdateService
{
    public class UpdateServiceCommand : IRequest<UpdateServiceDto>
    {
        public UpdateServiceDto UpdateServiceDto { get; set; }
    }
}
