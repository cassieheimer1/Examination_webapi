namespace WebApi_Examinationen.Models.Order
{
    public class Order
    {
        public string UserEmail { get; set; } = null!;
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string StreetAdress { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime DueDate { get; set; }
        public string OrderStatus { get; set; } = null!;


        public virtual List<OrderRow> OrderRows { get; set; } = null!;
    }

    public class OrderRow
    {
        public string ArticleNumber { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string ProductQuantity { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
