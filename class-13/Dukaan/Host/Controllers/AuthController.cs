using Dukaan.Application.Dtos;
using Dukaan.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dukaan.Host.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(IAuthService authService): ControllerBase
{
    public async Task<IActionResult> Login(LoginRequestDTO requset)
    {
        var result = await authService.LoginAsync(requset);
        return Ok(result);
    }
}