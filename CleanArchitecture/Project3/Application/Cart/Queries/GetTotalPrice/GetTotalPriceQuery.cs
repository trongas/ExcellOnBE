using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cart.Queries.GetTotalPrice
{
    public class GetTotalPriceQuery : IRequest<double>
    {
        public int Id { get; set; }
    }
}
