namespace WebApi.DTOs.Authentication
{
    public class ConfirmEmailDto
    {
        public string token { get; set; }

        public string email { get; set; }
    }
}
