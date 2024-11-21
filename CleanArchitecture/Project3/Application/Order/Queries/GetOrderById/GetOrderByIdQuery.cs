using Application.DTOs.Order;
using MediatR;

namespace Application.Order.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<GetOrderDto>
    {
        public int OrderId { get; set; }
    }
}
