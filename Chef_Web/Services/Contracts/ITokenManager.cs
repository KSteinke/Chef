namespace Chef_Web.Services.Contracts
{
    public interface ITokenManager
    {
        public Task SetTokenAsync(string token);
        public Task<string> GetTokenAsync();

        public Task DisposeToken();
    }
}
