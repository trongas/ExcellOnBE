using Application.DTOs.Cart;
using Domain.Abstraction;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Cart.Commands.DeleteCartItem
{
    public class DeleteCartItemCommandHandle : IRequestHandler<DeleteCartItemCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartItemCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartToDelete = await _unitOfWork.Cart.GetById(request.CartId);
                if (cartToDelete != null)
                {
                    await _unitOfWork.Cart.Delete(cartToDelete);
                    // Additional logic specific to service charge deletion can be added here

                    await _unitOfWork.CompleteAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

