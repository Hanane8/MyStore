using Domain_Layer.Models;
using Infrastructure_Layer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Application_Layer.Helpers;
using Application_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Infrastructure_Layer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DatabaseContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly TokenHelper _tokenHelper;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext dbContext, PasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;

            // Use IConfiguration to initialize TokenHelper
            _tokenHelper = new TokenHelper(configuration, userManager);
        }

        public async Task<IdentityResult> CreateUserAsync(User newuser, string password)
        {
            if (string.IsNullOrWhiteSpace(newuser.UserName))
            {
                newuser.UserName = newuser.Email;
            }

            var result = await _userManager.CreateAsync(newuser, password);

            return result;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> VerifyPasswordAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);
            return result.Succeeded;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            return await _tokenHelper.GenerateJwtTokenAsync(user);
        }

        public async Task<User?> GetUserByTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            // Validate the token and extract claims
            var claimsPrincipal = _tokenHelper.ValidateJwtToken(token);

            if (claimsPrincipal == null)
            {
                Console.WriteLine("Token validation failed.");
                return null;
            }

            var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("User ID not found in token claims.");
                return null;
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                Console.WriteLine($"User found: {user.Id}");
            }
            else
            {
                Console.WriteLine("User not found");
            }

            return user;
        }

        public async Task<IdentityResult> UpdateUserAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Users.ToListAsync(cancellationToken);
        }

        private async Task AssignRoleAsync(User user, string role)
        {
            if (!await _userManager.IsInRoleAsync(user, role))
            {
                var result = await _userManager.AddToRoleAsync(user, role);
                if (!result.Succeeded)
                {
                    Console.WriteLine($"Fel vid tilldelning av roll: {role}");
                }
            }
        }

        public async Task<IdentityResult> RemoveRoleAsync(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
