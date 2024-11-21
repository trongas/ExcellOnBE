using Application.DTOs.Order;
using MediatR;

namespace Application.Order.Queries.GetOrder
{
    public class GetOrderQuery : IRequest<ICollection<GetOrderDto>>
    {
    }
}
