using MagicVilla_Utility;
using MagicVilla_WebProject.Models;
using MagicVilla_WebProject.Models.Dto;
using MagicVilla_WebProject.Services.IServices;

namespace MagicVilla_WebProject.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl;

        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/UserAuth/login" //Note that the 'login' is named in the [HttpPost("login")] in the MagicVilla_VillaAPI UserController Login Action
            });
        }

        public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = obj,
                Url = villaUrl + "/api/UserAuth/register" //Note that the 'login' is named in the [HttpPost("register")] in the MagicVilla_VillaAPI UserController Register Action
            });
        }
    }
}