using IdentityServer.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService) => 
            _authService = authService;


        [HttpGet("inventoryapp")]
        public async Task<IActionResult> GetInventoryAppAccounts()
        {
            var accounts = await _authService.GetInventoryAppUsersAsync();

            return Ok(accounts);
        }

        [HttpPut("inventoryapp")]
        public async Task<IActionResult> SaveInventoryAppChanges([FromBody] IList<Account> accounts)
        {
            await _authService.SaveInventoryAppChangesAsync(accounts);

            return NoContent();
        }

        [HttpGet("checkerapp")]
        public async Task<IActionResult> GetCheckerAppAccounts()
        {
            var accounts = await _authService.GetCheckerAppUsersAsync();

            return Ok(accounts);
        }

        [HttpPut("checkerapp")]
        public async Task<IActionResult> SaveCheckerAppChanges([FromBody] IList<Account> accounts)
        {
            await _authService.SaveCheckerAppChangesAsync(accounts);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFullName(string id)
        {
            var name = await _authService.GetUserFullName(id);
            return Ok(name);
        }
    }
}
