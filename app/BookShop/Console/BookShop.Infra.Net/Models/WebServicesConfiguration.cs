using System.Collections.Generic;

namespace BookShop.Infra.Net.Models
{
    public class WebServicesConfiguration
    {
        public IEnumerable<WebService> List { get; set; }
    }
}