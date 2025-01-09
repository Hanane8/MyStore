using Application_Layer.Helpers;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(string email, string password);
        Task<string> GenerateJwtTokenAsync(User user, TokenHelper tokenHelper);
        Task<IdentityResult> CreateUserAsync(User user, string password);
        Task<IdentityResult> UpdateUserAsync(User user);
        Task<IdentityResult> DeleteUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
