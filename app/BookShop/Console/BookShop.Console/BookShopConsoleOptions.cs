using System;
using CommandLine;

namespace BookShop.Console
{
    public class BookShopConsoleOptions
    {
        [Option('n', "number", Required = false, HelpText = "Set number of messages.")]
        public int NumberMessages { get; set; }

        public BookShopConsoleOptions()
        {
            Random rnd = new Random();
            NumberMessages = rnd.Next(1, 10);
        }
    }
}