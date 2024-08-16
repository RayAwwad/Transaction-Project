using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using TransactionProject.Models;
using Transactions.Application.Interfaces;
using Transactions.Domain.Entities;

namespace TransactionProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUserAuth userAuth;

        public AccessController(IUserAuth userAuth)
        {
            this.userAuth = userAuth;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserDto userdto)
        {
            return await userAuth.SignUpAsync(userdto);
           
        }


        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginDto loginDto)
        {
            return await userAuth.LogInAsync(loginDto);
        }


    }
}
