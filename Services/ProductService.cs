using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi_Examinationen.Models;
using WebApi_Examinationen.Models.Product;


namespace WebApi_Examinationen.Services
{
        public interface IProductService
        {
            public Task<Product> CreateAsync(Product product);
            public Task<IEnumerable<Product>> GetAll();
            public Task<bool> DeleteAsync(string articleNumber);
            //public Task UpdateProductAsync(int id, Product request);
            Task<Product> UpdateProductAsync(int id, Product request);
    }

    public class ProductService : IProductService
        {
            private readonly DataBaseContext _context;
            private readonly IMapper _map;

            public ProductService(DataBaseContext context, IMapper map)
            {
            _context = context;
                _map = map;
            }


            public async Task<Product> CreateAsync(Product product)
            {
                if (!await _context.Products.AnyAsync(x => x.ArticleNumber == product.ArticleNumber))
                {
                    var productEntity = _map.Map<ProductEntity>(product);

                    var CategoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == product.CategoryName);
                    if (CategoryEntity == null)
                        productEntity.Category = new CategoryEntity { Name = product.CategoryName };
                    else
                        productEntity.CategoryId = CategoryEntity.Id;

                    await _context.Products.AddAsync(productEntity);
                    await _context.SaveChangesAsync();

                    return _map.Map<Product>(productEntity);
                }

                return null!;
            }

        public async Task<Product> UpdateProductAsync(int id, Product request)
        {
            var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (productEntity != null)
            {
                productEntity.ArticleNumber = request.ArticleNumber;
                productEntity.Name = request.ProductName;
                productEntity.Price = request.Price;
                productEntity.Description = request.Description;
                productEntity.Category.Name = request.CategoryName;
                _context.Entry(productEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new Product(productEntity.ArticleNumber, productEntity.Name, productEntity.Description, productEntity.Price, productEntity.Category.Name);

            }

            return null!;

        

        }

        public async Task<bool> DeleteAsync(string articleNumber)
            {
                var productEntity = await _context.Products.FindAsync(articleNumber);
                if (productEntity != null)
                {
                _context.Products.Remove(productEntity);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }

            public async Task<IEnumerable<Product>> GetAll()
            {
                return _map.Map<IEnumerable<Product>>(await _context.Products.Include(x => x.Category).ToListAsync());
            }
        }
    
}
