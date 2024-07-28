using System.ComponentModel.DataAnnotations;

namespace ASP_NET.Entities
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public List<Product> Products { get; set; }

    }
}
