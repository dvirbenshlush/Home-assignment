using Home_Assignment.Dal;
using Home_Assignment.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home_Assignment
{
    public class Program
    {
        public static void Main(string[] args)
        {


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public void ReadData()
        //{
        //    var cache = RedisConnectorHelper.Connection.GetDatabase();
        //    var devicesCount = 10000;
        //    for (int i = 0; i < devicesCount; i++)
        //    {
        //        var value = cache.StringGet($"Device_Status:{i}");
        //        Console.WriteLine($"Valor={value}");
        //    }
        //}

        //public void SaveBigData()
        //{
        //    var devicesCount = 10000;
        //    var rnd = new Random();
        //    var cache = RedisConnectorHelper.Connection.GetDatabase();

        //    for (int i = 1; i < devicesCount; i++)
        //    {
        //        var value = rnd.Next(0, 10000);
        //        cache.StringSet($"Device_Status:{i}", value);
        //    }
        //}
    }
}
