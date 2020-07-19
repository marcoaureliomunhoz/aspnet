using System.Collections.Generic;
using System.Linq;

namespace BookShop.Domain.Common
{
    public class Result<TResponse>
    {
        public Result()
        {
        }

        public Result(TResponse data)
        {
            Data = data;
        }

        public Result(TResponse data, string message)
        {
            Data = data;
            AddMessage(message);
        }

        public Result(string message)
        {
            _messages = new List<string>();
            _messages.Add(message);
        }

        private List<string> _messages = new List<string>();
        public IList<string> Messages => _messages;
        public bool HasMessage => _messages.Any();
        public TResponse Data { get; set; }

        public void AddMessage(string message) => _messages.Add(message);
    }

    public static class ResultFactory
    {
        public static Result<bool> Ok() => new Result<bool>(true);
        public static Result<bool> Nok() => new Result<bool>(false);
        public static Result<bool> Error(string message) => new Result<bool>(false, message);
    }

    // public class Result<TResponse> : Result
    // {
    //     public Result(TResponse data)
    //     {
    //         Data = data;
    //     }

    //     public Result(TResponse data, string message)
    //     {
    //         Data = data;
    //         AddMessage(message);
    //     }

    //     public TResponse Data { get; private set; }
    // }
}