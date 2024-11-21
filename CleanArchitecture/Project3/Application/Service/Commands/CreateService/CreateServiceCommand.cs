using Application.DTOs.Service;
using MediatR;

namespace Application.Service.Commands.CreateService
{
    public class CreateServiceCommand : IRequest<CreateServiceDto>
    {
        public CreateServiceDto ServiceDto { get; set; }
    }
}
