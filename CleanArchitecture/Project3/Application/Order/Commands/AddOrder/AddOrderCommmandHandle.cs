using Domain.Abstraction;
using MediatR;

namespace Application.Order.Commands.AddOrder
{
    public class AddOrderCommmandHandle : IRequestHandler<AddOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddOrderCommmandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newOrder = new Domain.Entities.Order()
                {
                    OrderDate = request.AddOrderDto.OrderDate,
                    OrderStatus = request.AddOrderDto.OrderStatus,
                    OrderTotal = request.AddOrderDto.OrderTotal,
                    ClientId = request.AddOrderDto.ClientId,
                };

                bool isCreate = await _unitOfWork.Order.Add(newOrder);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    return newOrder.OrderId;
                }

                return -1; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1; // Or any other value to indicate failure
            }
        }
    }
}
