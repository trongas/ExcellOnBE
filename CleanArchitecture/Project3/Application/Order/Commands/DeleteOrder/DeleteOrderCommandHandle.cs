using Application.Cart.Commands.DeleteCartItem;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Order.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandle : IRequestHandler<DeleteOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cartToDelete = await _unitOfWork.Order.GetById(request.OrderId);
                if (cartToDelete != null)
                {
                    await _unitOfWork.Order.Delete(cartToDelete);
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
