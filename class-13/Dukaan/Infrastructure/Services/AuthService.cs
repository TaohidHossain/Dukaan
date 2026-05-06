using System.Text;
using Dukaan.Application.Dtos;
using Dukaan.Domain.Interfaces;
using Dukaan.Infrastructure.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Dukaan.Infrastructure.Services;

public class AuthService(UserManager<Merchant> userManager, IConfiguration configuration): IAuthService
{
    public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new BadHttpRequestException("Invalid Credentials");
        if(!await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new UnauthorizedAccessException("Invalid Credentials");
        }

        return GenerateToken(user);
    }

    private AuthResponseDTO GenerateToken(Merchant user)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"]!));
    }
}