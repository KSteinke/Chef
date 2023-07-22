using Microsoft.JSInterop;

namespace Chef_Web.Services.Contracts
{
    public interface ICookieManager
    {
        public Task<T> GetValueAsync<T>(string key);
        public Task SetValueAsync<T>(string key, T value);
    }
}
