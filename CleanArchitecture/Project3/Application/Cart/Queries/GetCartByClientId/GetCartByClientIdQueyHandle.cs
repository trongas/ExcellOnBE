using Application.DTOs.Cart;
using Domain.Abstraction;
using MediatR;

namespace Application.Cart.Queries.GetCartByClientId
{
    public class GetCartByClientIdQueyHandle : IRequestHandler<GetCartByClientIdQuery, List<GetCartServiceChargeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartByClientIdQueyHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetCartServiceChargeDto>> Handle(GetCartByClientIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cartDetails = await _unitOfWork.Cart.GetCartById(request.Id);
                List<GetCartServiceChargeDto> serviceChargesDto = new List<GetCartServiceChargeDto>();

                foreach (var cartDetail in  cartDetails)
                {
                    var serviceCharge = await _unitOfWork.ServicesCharges.GetById(cartDetail.ServiceChargeId);

                    if (serviceCharge != null)
                    {
                        var serviceChargeDto = new GetCartServiceChargeDto()
                        {
                            ClientId = request.Id,
                            CartId = cartDetail.CartId,
                            ServiceChargeId = serviceCharge.ServiceChargesId,
                            ServiceChargesName = serviceCharge.ServiceChargesName,
                            Price = serviceCharge.Price,
                            ServiceChargesDescription = serviceCharge.ServiceChargesDescription,
                        };
                        serviceChargesDto.Add(serviceChargeDto);
                    }
                }
                return serviceChargesDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Query: " + ex);
                return null;
            }
        }
    }
}
