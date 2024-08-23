using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionProject.Data;
using TransactionProject.Models;
using Transactions.Application.Interfaces;
using Transactions.Application.Services;
using Transactions.Domain.Entities;

namespace TransactionProject.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserInfoService userInfoService;
      

        public UserController(IUserInfoService userInfoService)
        {
            this.userInfoService = userInfoService;

        }




        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            return await userInfoService.UpdateUserAsync(id, updateUserDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return await userInfoService.GetUserByIdAsync(id);
        }
    }
}