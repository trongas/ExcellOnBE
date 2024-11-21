using Application.DTOs.Order;
using MediatR;


namespace Application.Order.Queries.GetOrderByClientId
{
    public class GetOrderByClientIdQuery : IRequest<List<GetOrderDto>>
    {
        public int ClientId { get; set; }
    }
}
