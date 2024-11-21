using Application.DTOs.ServiceCharges;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Service
{
    public class GetServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }       
        public string Description { get; set; }
        public int TotalDay { get; set; }
        //public List<GetServiceChargesDto> ServiceCharges { get; set; }

    }
}
