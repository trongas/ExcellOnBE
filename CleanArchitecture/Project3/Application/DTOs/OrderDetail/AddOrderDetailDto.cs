using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.OrderDetail
{
    public class AddOrderDetailDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ServiceChargesId { get; set; }
    }
}
