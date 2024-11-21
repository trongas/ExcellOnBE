using Application.DTOs.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Order
{
    public class AddOrderDto
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public double OrderTotal { get; set; }

        public int ClientId { get; set; }
    }
}
