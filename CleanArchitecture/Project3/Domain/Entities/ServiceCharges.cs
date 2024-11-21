using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ServiceCharges
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceChargesId { get; set; }
        [Required]
        public string ServiceChargesName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ServiceChargesDescription { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public Service Service { get; set; }
        public ICollection<CartDetail> CartDetail { get; set; }

    }

}
