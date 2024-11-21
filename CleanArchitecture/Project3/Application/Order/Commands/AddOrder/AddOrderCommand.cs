using Application.DTOs.Order;
using MediatR;


namespace Application.Order.Commands.AddOrder
{
    public class AddOrderCommand : IRequest<int>
    {
        public AddOrderDto AddOrderDto { get; set; }
    }
}
