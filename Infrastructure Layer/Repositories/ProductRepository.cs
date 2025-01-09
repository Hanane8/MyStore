using Application_Layer.Interfaces;
using Domain_Layer.Models;
using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Layer.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductRepository(DatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                                   .Where(p => p.CategoryId == categoryId)
                                   .Include(p => p.Category)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice, CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                                   .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                                   .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Products
                .Include(p => p.Category) 
                .ToListAsync(cancellationToken);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
