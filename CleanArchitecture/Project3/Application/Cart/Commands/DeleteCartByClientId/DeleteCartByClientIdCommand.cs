using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Commands.DeleteCartByClientId
{
    public class DeleteCartByClientIdCommand : IRequest<bool>
    {
        public int ClientId { get; set; }
    }
}
