using Application.DTOs.ServiceCharges;
using MediatR;


namespace Application.ServiceCharges.Queries.GetServiceChargesById
{
    public class GetServiceChargesByIdQuery : IRequest<GetServiceChargesDto>
    {
        public int ServiceChargesId { get; set; }
    }
}
