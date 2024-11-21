using Application.DTOs.Cart;
using Application.DTOs.Order;
using Application.DTOs.OrderDetail;
using Domain.Abstraction;
using MediatR;

namespace Application.Order.Queries.GetOrderByClientId
{
    public class GetOrderByClientIdQueryHandle : IRequestHandler<GetOrderByClientIdQuery, List<GetOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByClientIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetOrderDto>> Handle(GetOrderByClientIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWork.Order.GetOrderByClientId(request.ClientId);
                List<GetOrderDto> orderDtos = new List<GetOrderDto>();

                foreach (var order in orders)
                {
                    GetOrderDto orderDto = new GetOrderDto
                    {
                        OrderId = order.OrderId,
                        OrderDate = order.OrderDate,
                        OrderStatus = order.OrderStatus,
                        OrderTotal = order.OrderTotal,
                        ClientId = order.ClientId,
                        OrderDetail = new List<GetOrderDetailDto>(),
                    };

                    var orderDetails = await _unitOfWork.OrderDetail.GetOrderDetailsByOrderId(order.OrderId);

                    foreach (var orderItem in orderDetails)
                    {
                        var orderDetailDto = new GetOrderDetailDto
                        {
                            OrderDetailId = orderItem.OrderDetailId,
                            ServiceChargeId = orderItem.ServiceChargesId,
                            OrderId = orderItem.OrderId,
                        };
                        orderDto.OrderDetail.Add(orderDetailDto);
                    }

                    orderDtos.Add(orderDto);
                }

                return orderDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while processing order query: " + ex.Message);
                throw; 
            }
        }
    }
}
