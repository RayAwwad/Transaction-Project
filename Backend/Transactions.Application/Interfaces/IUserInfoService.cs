using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;

namespace Transactions.Application.Interfaces
{
    public interface IUserInfoService
    {
        Task<IActionResult> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<IActionResult> GetUserByIdAsync(int id);
    }
}
