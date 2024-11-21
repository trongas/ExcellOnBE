using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public double OrderTotal { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [Required]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
