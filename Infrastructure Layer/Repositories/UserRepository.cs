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

namespace Infrastructure_Layer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DatabaseContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;


        // Lägg till konstruktor för loggning om det behövs
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext dbContext, PasswordHasher<User> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
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

        public async Task<string> GenerateJwtTokenAsync(User user, TokenHelper tokenHelper)
        {
            return await tokenHelper.GenerateJwtTokenAsync(user);
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

        //private async Task AssignRoleAsync(User user, string role)
        //{
        //    if (!await _userManager.IsInRoleAsync(user, role))
        //    {
        //        var result = await _userManager.AddToRoleAsync(user, role);
        //        if (!result.Succeeded)
        //        {
        //            // Logga eller hantera fel om det behövs
        //            Console.WriteLine($"Fel vid tilldelning av roll: {role}");
        //        }
        //    }
        //}

        public async Task<IdentityResult> RemoveRoleAsync(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
