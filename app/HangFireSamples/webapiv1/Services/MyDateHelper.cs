using System;
using webapiv1.Interfaces;

namespace webapiv1.Services
{
    public class MyDateHelper: IMyDateHelper
    {
        public void GetCurrentDate()
        {
            Console.WriteLine($"Data corrente: {DateTime.Now}");
        }
    }
}