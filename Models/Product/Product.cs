namespace WebApi_Examinationen.Models.Product
{
    public class Product
    {
        public Product()
        {

        }
        public Product(string articleNumber, string productName, string description, decimal price, string categoryName)
        {
        }

        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
