using System.Collections.Generic;

namespace BookShop.Infra.Net.Models
{
    public class WebService
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }

    public class Service
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public IEnumerable<Action> Actions { get; set; }
    }

    public class Action
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }

    public static class ActionsType
    {
        public const string Get = "Get";
        public const string Post = "Post";
    }
}