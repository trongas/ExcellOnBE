using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Service
{
    public class CreateServiceDto
    {
        
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string Description { get; set; }       
        public int TotalDay { get; set; }
    }
}
