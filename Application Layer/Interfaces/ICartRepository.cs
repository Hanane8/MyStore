using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByUserIdAsync(string userId, CancellationToken cancellationToken);
        Task<Cart?> GetCartBySessionIdAsync(int sessionId, CancellationToken cancellationToken);
        Task AddCartAsync(Cart cart, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
