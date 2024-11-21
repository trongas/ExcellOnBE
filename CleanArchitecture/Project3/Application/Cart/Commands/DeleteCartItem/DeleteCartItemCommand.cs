﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Commands.DeleteCartItem
{
    public class DeleteCartItemCommand : IRequest<bool>
    {
       public int CartId { get; set; }
    }
}
