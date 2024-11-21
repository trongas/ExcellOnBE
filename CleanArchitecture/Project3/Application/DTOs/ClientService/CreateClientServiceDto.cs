using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ClientService
{
    public class CreateClientServiceDto
    {
        [Required]
        public string Status { get; set; }
        public string StartDay { get; set; }
        public string ExpiredDay { get; set; }
        [Required]
        public int ClientId { get; set; }

        [Required]
        public int ServiceId { get; set; }   
    }
}
