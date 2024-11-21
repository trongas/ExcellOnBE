using Application.DTOs.OrderDetail;
using Domain.Abstraction;
using MediatR;


namespace Application.OrderDetail.Commands.CreateOrderDetail
{
    public class CreateOrderDetailCommandHandle : IRequestHandler<CreateOrderDetailCommand, AddOrderDetailDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderDetailCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddOrderDetailDto> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newOrder = new Domain.Entities.OrderDetail()
                {
                    OrderId = request.AddOrderDetailDto.OrderId,
                    ServiceChargesId = request.AddOrderDetailDto.ServiceChargesId,

                };

                bool isCreate = await _unitOfWork.OrderDetail.Add(newOrder);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    // Additional logic can be added here if needed
                    return request.AddOrderDetailDto;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
