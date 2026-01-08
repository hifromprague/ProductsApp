namespace ProductsApp.Models
{
    public class ProductItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Count { get; set; }
    }
}
