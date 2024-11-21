using Application.DTOs.Order;
using Application.DTOs.OrderDetail;
using Domain.Abstraction;
using MediatR;

namespace Application.Order.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandle : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWork.Order.GetById(request.OrderId);

                if (order == null)
                {
                    return null;
                }
               


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

                foreach (var orderDetail in orderDetails)
                {
                    var orderDetailDto = new GetOrderDetailDto
                    {
                        // Map properties from orderDetail to orderDetailDto
                        OrderDetailId = orderDetail.OrderDetailId,
                        ServiceChargeId = orderDetail.ServiceChargesId,
                        OrderId = orderDetail.OrderId,

                    };
                    orderDto.OrderDetail.Add(orderDetailDto);
                }

                return orderDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query : " + ex);
                return null;
            }
        }
    }
}
