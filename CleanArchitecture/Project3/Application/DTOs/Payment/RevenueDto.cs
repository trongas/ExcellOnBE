using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Payment
{
    public class RevenueDto
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
    }
}
