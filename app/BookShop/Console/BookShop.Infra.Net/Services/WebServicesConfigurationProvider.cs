using System.Collections.Generic;
using BookShop.Infra.Net.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BookShop.Infra.Net.Services
{
    public static class WebServicesConfigurationProvider
    {
        public static WebServicesConfiguration GetConfig(IConfiguration configuration)
        {
            return new WebServicesConfiguration
            {
                List = JsonConvert.DeserializeObject<IEnumerable<WebService>>(configuration.GetSection("WebServices").Value)
            };
        }
    }
}