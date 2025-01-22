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
                    .ThenInclude(item => item.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
            }

        //public async Task<Cart?> GetCartBySessionIdAsync(int sessionId, CancellationToken cancellationToken)
        //{
        //    return await _dbContext.Carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.SessionId == sessionId, cancellationToken);
        //}

        public async Task AddCartAsync(Cart cart, CancellationToken cancellationToken)
        {
            // Check if the user exists
            var userExists = await _dbContext.Users.AnyAsync(u => u.Id == cart.UserId, cancellationToken);
            if (!userExists)
            {
                throw new InvalidOperationException("User does not exist.");
            }

            // Check if the cart already exists
            var cartExists = await _dbContext.Carts.AnyAsync(c => c.Id == cart.Id, cancellationToken);
            if (cartExists)
            {
                throw new InvalidOperationException("Cart already exists.");
            }

            await _dbContext.Carts.AddAsync(cart, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
    }
    
}
