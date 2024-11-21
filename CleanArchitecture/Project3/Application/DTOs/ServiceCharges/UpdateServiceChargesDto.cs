using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ServiceCharges
{
    public class UpdateServiceChargesDto
    {
        public int ServiceChargesId { get; set; }

        public string ServiceChargesName { get; set; }

        public double Price { get; set; }

        public string ServiceChargesDescription { get; set; }

    }
}
