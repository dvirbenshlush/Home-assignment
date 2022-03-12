using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home_Assignment.Dal
{
    public class RedisService
    {
        private static void SaveData(string host, string key, string value)
        {
            using (RedisClient client = new RedisClient(host)) 
            {
                //if (client.Get<string>(key) == null)
                //{
                    client.Set(key, value);
                //}
            }
        }

        private static string ReadData(string host, string key)
        {
            using (RedisClient client = new RedisClient(host))
            {
                    return client.Get<string>(key);
            }   
        }



        public static bool isTheDurationTooShort(string action)
        {
            string lastQuery = RedisService.ReadData("localhost", action);
            if (lastQuery == null)
                RedisService.SaveData("localhost", action, DateTime.Now.ToString());
            lastQuery = RedisService.ReadData("localhost", action);
            TimeSpan duration = new TimeSpan(DateTime.Now.Ticks - DateTime.Parse(lastQuery).Ticks);

            if (duration.Seconds > 10)
            {
                RedisService.SaveData("localhost", action, DateTime.Now.ToString());
                return true;
            }
            return false;
        }
    }
}
