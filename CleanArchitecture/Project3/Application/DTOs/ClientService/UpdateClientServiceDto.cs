using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ClientService
{
    public class UpdateClientServiceDto
    {
        [Required]
        public int ClientServiceId { get; set; }
        public string Status { get; set; }
        public string StartDay { get; set; }
        public string ExpiredDay { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
    }
}
