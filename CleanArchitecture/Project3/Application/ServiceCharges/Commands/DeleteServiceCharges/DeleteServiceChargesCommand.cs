using MediatR;


namespace Application.ServiceCharges.Commands.DeleteServiceCharges
{
    public class DeleteServiceChargesCommand : IRequest<bool>
    {
        public int ServiceChargesId { get; set; }
    }
}
