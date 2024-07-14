using System.ComponentModel.DataAnnotations;

namespace ASP_NET.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public decimal Price { get; set; }
    }
}
