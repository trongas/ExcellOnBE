using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Cart
{
    public class GetCartServiceChargeDto
    {
        public int CartId { get; set; }
        public int ClientId { get; set; }
        public int ServiceChargeId { get; set; }
        public string ServiceChargesName { get; set; }

        public double Price { get; set; }

        public string ServiceChargesDescription { get; set; }
    }
}
