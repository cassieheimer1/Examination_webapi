namespace WebApi_Examinationen.Models.Product
{
    public class UpdateProduct
    {
        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
