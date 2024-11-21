using System.Threading;
using System.Threading.Tasks;
using Application.DTOs.Cart;
using Domain.Abstraction;
using MediatR;

namespace Application.Cart.Commands.AddCart
{
    public class AddToCartCommandHandle : IRequestHandler<AddToCartCommand, AddToCartDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddToCartCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddToCartDto> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newCart = new Domain.Entities.CartDetail()
                {
                    ClientId = request.AddToCartDto.ClientId,
                    ServiceChargeId = request.AddToCartDto.ServiceChargeId,

                };

                bool isCreate = await _unitOfWork.Cart.Add(newCart);

                if (isCreate)
                {
                    await _unitOfWork.CompleteAsync();
                    // Additional logic can be added here if needed
                    return request.AddToCartDto;
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
