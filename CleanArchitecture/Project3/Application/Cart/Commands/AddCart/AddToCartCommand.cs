using Application.DTOs.Cart;
using Application.DTOs.Client;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Commands.AddCart
{
    public class AddToCartCommand : IRequest<AddToCartDto>
    {
        public AddToCartDto AddToCartDto { get; set; }
    }
}
