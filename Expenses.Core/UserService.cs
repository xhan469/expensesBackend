using System;
using System.Threading.Tasks;
using Expenses.Core.CustomExceptions;
using Expenses.Core.DTO;
using Expenses.Core.Utilities;
using Expenses.DB;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Core
{
    public class UserService : IUserServices
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticatedUser> signIn(User user)
        {
            var dbUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username == user.Username);
            if(dbUser == null
                || _passwordHasher.VerifyHashedPassword(dbUser.Password, user.Password) == PasswordVerificationResult.Failed)
            {
                throw new InvalidUsernamePasswordException("Invalid username or password");
            }

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JWTGenerator.GenerateUserToken(user.Username)
            };
            
        }

        public async Task<AuthenticatedUser> signUp(User user)
        {
            var checkUser = await _context.User
                .FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if(checkUser != null)
            {
                throw new UsernameAlreadyExistException("Username already exists");
            }

            user.Password = _passwordHasher.HashPassword(user.Password);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return new AuthenticatedUser
            {
                Username = user.Username,
                Token = JWTGenerator.GenerateUserToken(user.Username)

            };
        }
    }
}

