using System.ComponentModel.DataAnnotations;

namespace OrderApplication.Models
{
    public class Order
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Description { get; set; }

        [Range(1, 20)]
        public int Quantity { get; set; }
    }
}
