using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;
using Transactions.Application.Interfaces;

namespace Transactions.Application.Services
{
    public class UserService :IUserInfoService
    {
        private readonly IUserInfoRepo userInfoRepo;

        public UserService(IUserInfoRepo userInfoRepo)
        {
            this.userInfoRepo=userInfoRepo;
        }

        public async Task<IActionResult> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await userInfoRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return new NotFoundResult();
            }

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.Balance = updateUserDto.Balance;

            await userInfoRepo.UpdateUserAsync(user);
            return new OkObjectResult(user);
        }

        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var user = await userInfoRepo.GetUserByIdAsync(id);
            if (user == null)
            {
                return new NotFoundResult();
            }

            var userDto = new
            {
                user.FirstName,
                user.LastName,
                user.Email,
                user.Balance,
            };
            return new OkObjectResult(userDto);
        }


    }
}
