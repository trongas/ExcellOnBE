using Application.DTOs.ServiceCharges;
using MediatR;


namespace Application.ServiceCharges.Commands.CreateServiceCharges
{
    public class CreateServiceChargesCommand : IRequest<CreateServiceChargesDto>
    {
        public CreateServiceChargesDto ServiceChargesDto { get; set; }
    }
}
