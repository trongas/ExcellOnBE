using Application.DTOs.Cart;
using Application.DTOs.Service;
using Application.DTOs.ServiceCharges;
using Application.ServiceCharges.Queries.GetAllServiceCharges;
using Domain.Abstraction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetCartId
{
    public class GetCartIdQueryHandle : IRequestHandler<GetCartIdQuery, ICollection<GetCartServiceChargeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCartIdQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ICollection<GetCartServiceChargeDto>> Handle(GetCartIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _unitOfWork.Cart.All();
            var cartDto = new List<GetCartServiceChargeDto>();

            foreach (Domain.Entities.CartDetail c in cart)
            {
                var cartDtos = new GetCartServiceChargeDto()
                {
                    CartId = c.CartId,
                    ServiceChargeId = c.ServiceChargeId,
                   ClientId = c.ClientId,                   
               };
                cartDto.Add(cartDtos);
            }

            return cartDto;
        }
    }
}
