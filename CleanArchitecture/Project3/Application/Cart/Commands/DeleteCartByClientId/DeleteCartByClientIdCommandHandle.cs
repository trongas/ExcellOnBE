using Application.Cart.Commands.DeleteCartItem;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Commands.DeleteCartByClientId
{
    public class DeleteCartByClientIdCommandHandle : IRequestHandler<DeleteCartByClientIdCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCartByClientIdCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCartByClientIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.Cart.DeleteCartByClientId(request.ClientId);
               
                  
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
