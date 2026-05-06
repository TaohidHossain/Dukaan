using Dukaan.Application.Dtos;

namespace Dukaan.Domain.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> LoginAsync(LoginRequestDTO request);
}