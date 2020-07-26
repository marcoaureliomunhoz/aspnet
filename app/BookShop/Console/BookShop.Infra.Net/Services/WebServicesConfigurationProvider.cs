using System.Collections.Generic;
using System.Linq;
using BookShop.Infra.Net.Models;
using Microsoft.Extensions.Configuration;

namespace BookShop.Infra.Net.Services
{
    public static class WebServicesConfigurationProvider
    {
        public static WebServicesConfiguration GetConfig(IConfiguration configuration)
        {
            var list = new List<WebService>();
            configuration.GetSection("WebServices").Bind(list);
            return new WebServicesConfiguration()
            {
                List = list
            };
        }
    }
}