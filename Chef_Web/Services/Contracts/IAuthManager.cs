using System.Security.Claims;

namespace Chef_Web.Services.Contracts
{
    public interface IAuthManager
    {
        
        public string AuthToken { get; set; }

        public IEnumerable<Claim>? Claims { get; set; }

        public void LogUser(string token);
        public void LogoutUser();
    }
}
