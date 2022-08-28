using AutoMapper;
using WebApi_Examinationen.Models;
using WebApi_Examinationen.Models.Order;
using WebApi_Examinationen.Models.Product;
using WebApi_Examinationen.Models.User;

namespace WebApi_Examinationen.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(List<Product> shoppingcart, User user);
    }

    public class OrderService : IOrderService
    {
        private readonly DataBaseContext _db;
        private readonly IMapper _map;

        public OrderService(DataBaseContext db, IMapper map)
        {
           _db = db;
           _map = map;
        }

        public async Task<Order> CreateAsync(List<Product> shoppingcart, User user)
        {
            var orderEntity = new OrderEntity
            {
               CustomerName = $"{user.FirstName} {user.LastName}",
               Address = $"{user.StreetAdress}, {user.PostalCode} {user.City}",
            };

            var orderRows = new List<OrderRowEntity>();
            foreach (var item in shoppingcart)
                orderRows.Add(new OrderRowEntity
                {
                    OrderId = orderEntity.Id,
                    ArticleNumber = item.ArticleNumber
                });

            orderEntity.OrderRows = orderRows;

            _db.Orders.Add(orderEntity);
            await _db.SaveChangesAsync();

            return null!;
        }
    }
}
