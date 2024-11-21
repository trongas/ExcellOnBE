using Application.DTOs.OrderDetail;
using MediatR;

namespace Application.OrderDetail.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommand : IRequest<AddOrderDetailDto>
    {
        public AddOrderDetailDto AddOrderDetailDto { get; set; }
    }
}
