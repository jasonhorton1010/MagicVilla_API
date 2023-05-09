using MagicVilla_WebProject.Models.Dto;

namespace MagicVilla_WebProject.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDTO objToCreate);

        Task<T> RegisterAsync<T>(RegistrationRequestDTO objToCreate);
    }
}
