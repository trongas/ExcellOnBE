using Application.DTOs.Cart;
using Application.DTOs.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class GetOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public int OrderStatus { get; set; }
        public int ClientId { get; set; }
        public ICollection<GetOrderDetailDto> OrderDetail { get; set; }

    }
}
