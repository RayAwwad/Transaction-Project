using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;

namespace Transactions.Application.Services
{
    public class AuthService : IUserAuth
    {
        private readonly IUserRepo repo;
        private readonly JwtHelper jwtHelper;

        public AuthService(IUserRepo repo, JwtHelper jwtHelper)
        {
            this.repo = repo;
            this.jwtHelper = jwtHelper;
        }

        public async Task<IActionResult> SignUpAsync(UserDto userDto)
        {
            var userExists = await repo.UserExistsAsync(userDto.Email);
            if (userExists)
            {
                return new BadRequestObjectResult("User already exists");
            }
           

            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(userDto.Password, salt);

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                PasswordHash = hashedPassword,
                Salt = salt,
                Balance = 0
            };

            await repo.AddUserAsync(user);
            var token = jwtHelper.GenerateToken(user.Id);

            return new OkObjectResult(new { user.FirstName, user.LastName, user.Email, user.Balance, Token = token });
        }

        public async Task<IActionResult> LogInAsync(LoginDto loginDto)
        {
            var user = await repo.GetEmailAsync(loginDto.Email);

            if (user == null)
                return new UnauthorizedObjectResult("Account not found");

            var isValidPassword = PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt);
            if (!isValidPassword)
                return new UnauthorizedObjectResult("Invalid password");

            var token = jwtHelper.GenerateToken(user.Id);

            return new OkObjectResult(new { user.FirstName, user.LastName, user.Email, user.Balance, Token = token });
        }
    }
}
