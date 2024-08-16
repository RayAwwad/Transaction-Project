using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using TransactionProject.Models;
using Transactions.Domain.Entities;

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