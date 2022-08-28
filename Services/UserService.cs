using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi_Examinationen.Models;
using WebApi_Examinationen.Models.Product;
using WebApi_Examinationen.Models.User;

namespace WebApi_Examinationen.Services
{
        public interface IUserService
        {
            public Task<Product> CreateProductAsync(ProductRequest request);
            public Task<Product> GetProductAsync(int id);
            public Task<IEnumerable<Product>> GetAllProductsAsync();
            public Task<Product> UpdateProductAsync(int id, ProductRequest request);
            public Task<bool> DeleteProductAsync(int id);

            public Task<User> CreateUserAsync(UserRequest request);
            public Task<User> GetUserAsync(int id);
            public Task<IEnumerable<User>> GetAllUsersAsync();
            public Task<User> UpdateUserAsync(int id, UserRequest request);
            public Task<bool> DeleteUserAsync(int id);
        }


        public class UserService : IUserService
        {
            private readonly DataBaseContext _context;
            private readonly IMapper _mapper;

            public UserService(DataBaseContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<User> CreateUserAsync(UserRequest request)
            {
                if (!await _context.Users.AnyAsync(x => x.Email == request.Email))
                {
                    var userEntity = _mapper.Map<UserEntity>(request);
                    //userEntity.CreateSecurePassword();

                    _context.Users.Add(userEntity);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<User>(userEntity);
                }

                return null!;
            }

            public async Task<Product> CreateProductAsync(ProductRequest request)
            {
                if (!await _context.Products.AnyAsync(x => x.Name == request.ProductName))
                {
                    var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == request.CategoryName);
                    if (categoryEntity == null)
                    {
                        categoryEntity = new CategoryEntity { Name = request.CategoryName };
                        _context.Categories.Add(categoryEntity);
                        await _context.SaveChangesAsync();
                    }

                    var productEntity = _mapper.Map<ProductEntity>(request);
                    productEntity.CategoryId = categoryEntity.Id;

                    _context.Products.Add(productEntity);
                    await _context.SaveChangesAsync();

                    return _mapper.Map<Product>(productEntity);
                }

                return null!;
            }

            public async Task<bool> DeleteUserAsync(int id)
            {
                var userEntity = await _context.Users.FindAsync(id);
                if (userEntity != null)
                {
                    _context.Users.Remove(userEntity);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }

            public async Task<bool> DeleteProductAsync(int id)
            {
                var productEntity = await _context.Products.FindAsync(id);
                if (productEntity != null)
                {
                    _context.Products.Remove(productEntity);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }

            public async Task<IEnumerable<User>> GetAllUsersAsync()
            {
                return _mapper.Map<IEnumerable<User>>(await _context.Users.ToListAsync());
            }

            public async Task<IEnumerable<Product>> GetAllProductsAsync()
            {
                return _mapper.Map<IEnumerable<Product>>(await _context.Products.Include(x => x.Category).ToListAsync());
            }

            public async Task<User> GetUserAsync(int id)
            {
                return _mapper.Map<User>(await _context.Users.FirstOrDefaultAsync(x => x.Id == id));
            }

            public async Task<Product> GetProductAsync(int id)
            {
                return _mapper.Map<Product>(await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id));
            }

            public async Task<User> UpdateUserAsync(int id, UserRequest request)
            {
                var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (userEntity != null)
                {
                    if (userEntity.FirstName != request.FirstName && !string.IsNullOrEmpty(request.FirstName))
                        userEntity.FirstName = request.FirstName;

                    if (userEntity.LastName != request.LastName && !string.IsNullOrEmpty(request.LastName))
                        userEntity.LastName = request.LastName;

                    if (userEntity.Email != request.Email && !string.IsNullOrEmpty(request.Email))
                        userEntity.Email = request.Email;

                    _context.Entry(userEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return _mapper.Map<User>(userEntity);
                }

                return null!;
            }

            public async Task<Product> UpdateProductAsync(int id, ProductRequest request)
            {
                var productEntity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
                if (productEntity != null)
                {
                    if (productEntity.Name != request.ProductName && !string.IsNullOrEmpty(request.ProductName))
                        productEntity.Name = request.ProductName;

                    if (productEntity.Price != request.Price)
                        productEntity.Price = request.Price;

                    if (productEntity.Category.Name != request.CategoryName && !string.IsNullOrEmpty(request.CategoryName))
                    {
                        var categoryEntity = await _context.Categories.FirstOrDefaultAsync(x => x.Name == request.CategoryName);
                        if (categoryEntity == null)
                        {
                            categoryEntity = new CategoryEntity { Name = request.CategoryName };
                            _context.Categories.Add(categoryEntity);
                            await _context.SaveChangesAsync();
                        }

                        productEntity.CategoryId = categoryEntity.Id;
                    }

                    _context.Entry(productEntity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return _mapper.Map<Product>(productEntity);
                }

                return null!;

            }
        }
    
}
