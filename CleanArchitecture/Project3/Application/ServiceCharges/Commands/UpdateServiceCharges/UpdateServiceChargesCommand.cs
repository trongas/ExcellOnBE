using Application.DTOs.ServiceCharges;
using MediatR;


namespace Application.ServiceCharges.Commands.UpdateServiceCharges
{
    public class UpdateServiceChargesCommand : IRequest<UpdateServiceChargesDto>
    {
        public UpdateServiceChargesDto UpdateServiceChargesDto { get; set; }
    }
}
