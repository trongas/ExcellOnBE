using Application.DTOs.ServiceCharges;
using MediatR;


namespace Application.ServiceCharges.Queries.GetAllServiceCharges
{
    public class GetAllServiceChargesQuery : IRequest<ICollection<GetServiceChargesDto>>
    {
    }
}
