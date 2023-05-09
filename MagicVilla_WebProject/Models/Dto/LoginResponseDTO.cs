namespace MagicVilla_WebProject.Models.Dto
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }

        public string Token { get; set; }
    }
}