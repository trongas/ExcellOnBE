using Application.DTOs.Cart;
using Application.DTOs.ServiceCharges;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetCartByClientId
{
    public class GetCartByClientIdQuery : IRequest<List<GetCartServiceChargeDto>>
    {
        public int Id { get; set; }
    }
}
