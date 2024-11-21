using Application.DTOs.ServiceCharges;
using MediatR;

namespace Application.ServiceCharges.Queries.GetServiceChargesByServiceId
{
    public class GetServiceChargeByServiceIdQuery : IRequest<List<GetServiceChargesDto>>
    {
        public int Id { get; set; }
    }
}
