using System.Threading.Tasks;
using BookShop.Infra.Net.Interfaces;
using Flurl;
using Flurl.Http;

namespace BookShop.Infra.Net.Services
{
    public class HttpService : IHttpService
    {
        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            return await url
                .GetJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostAsync<TBody, TResponse>(string url, TBody body)
        {
            return await url
                .PostJsonAsync(body)
                .ReceiveJson<TResponse>();
        }

        public async Task PostAsync<TBody>(string url, TBody body)
        {
            await url
                .PostJsonAsync(body);
        }
    }
}