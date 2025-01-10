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
    public class CartRepository : ICartRepository
    {
        private readonly DatabaseContext _dbContext;

        public CartRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

            public async Task<Cart?> GetCartByUserIdAsync(string userId, CancellationToken cancellationToken)
            {
                return await _dbContext.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
            }

            public async Task<Cart?> GetCartBySessionIdAsync(int sessionId, CancellationToken cancellationToken)
            {
                return await _dbContext.Carts
                    .Include(c => c.Items)
                    .FirstOrDefaultAsync(c => c.SessionId == sessionId, cancellationToken);
            }

            public async Task AddCartAsync(Cart cart, CancellationToken cancellationToken)
            {
                await _dbContext.Carts.AddAsync(cart, cancellationToken);
            }

            public async Task SaveChangesAsync(CancellationToken cancellationToken)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
    }
    
}
