using System.Collections.Generic;

namespace BookShop.Infra.Net.Models
{
    public class WebServicesConfiguration
    {
        public IList<WebService> List { get; set; }

        public WebServicesConfiguration()
        {
            List = new List<WebService>();
        }
    }
}