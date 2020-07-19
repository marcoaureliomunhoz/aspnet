using System.Threading.Tasks;

namespace BookShop.Infra.Net.Interfaces
{
    public interface IHttpService
    {
        Task<TResponse> GetAsync<TResponse>(string url);
        Task<TResponse> PostAsync<TBody, TResponse>(string url, TBody body);
        Task PostAsync<TBody>(string url, TBody body);
    }
}