using System.ComponentModel.DataAnnotations;

namespace ASP_NET.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public List<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
