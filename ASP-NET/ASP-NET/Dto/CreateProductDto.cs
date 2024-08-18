namespace ASP_NET.Dto
{
    public class CreateProductDto
    {
        public string Title { get; set; }

        public IFormFile Photo { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }
    }
}
