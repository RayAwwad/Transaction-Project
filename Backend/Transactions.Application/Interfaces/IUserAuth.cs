using Microsoft.AspNetCore.Mvc;
using TransactionProject.Models;

namespace Transactions.Application.Interfaces
{
    public interface IUserAuth
    {
        Task<IActionResult> SignUpAsync(UserDto userDto);
        Task<IActionResult> LogInAsync(LoginDto loginDto);
    }
}
