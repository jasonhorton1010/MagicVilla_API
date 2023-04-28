using MagicVilla_WebProject.Models;

namespace MagicVilla_WebProject.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel{ get; set; }

        Task<T> SendAsync<T>(APIRequest aPIRequest);
    }
}