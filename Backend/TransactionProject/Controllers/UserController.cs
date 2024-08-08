using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using TransactionProject.Models;
using TransactionProject.Models.Entities;

namespace TransactionProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext DbContext;
        private readonly IConfiguration configuration;
        private readonly JwtHelper jwtHelper;

        public UserController(ApplicationDbContext DbContext, IConfiguration configuration, JwtHelper jwtHelper)
        {
            this.DbContext = DbContext;
            this.configuration = configuration;
            this.jwtHelper = jwtHelper;
        }

        
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserDto userdto)
        {
            var userExists = await DbContext.Users.AnyAsync(x => x.Email == userdto.Email);
            if (userExists)
                return BadRequest("User already exists");

            var salt = PasswordHasher.GenerateSalt();
            var hashedPassword = PasswordHasher.HashPassword(userdto.Password, salt);

            var user = new User
            {
                FirstName = userdto.FirstName,
                LastName = userdto.LastName,
                Email = userdto.Email,
                PasswordHash = hashedPassword,
                Salt = salt,
                Balance = 0 
            };

            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
            var token = jwtHelper.GenerateToken(user.Id);

            return Ok(new { user.FirstName, user.LastName, user.Email, user.Balance, Token = token });
        }

      
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {

            var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Account not found");
            }

            var isValidPassword = PasswordHasher.VerifyPassword(loginDto.Password, user.PasswordHash, user.Salt);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid password");
            }

            var token = jwtHelper.GenerateToken(user.Id);

            return Ok(new { user.FirstName, user.LastName, user.Email, user.Balance, Token = token });
        }



  
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = await DbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.Balance = updateUserDto.Balance;

            await DbContext.SaveChangesAsync();
            return Ok(user);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserbyid(int id)
        {
            var user = await DbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var newuser = new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.Balance,
            };
            return Ok(newuser);
        }
    }
}