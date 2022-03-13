using Home_Assignment.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Home_Assignment.Dal
{
    public class RedisService
    {
        private static void SaveData(string host, string key, string value)
        {
            using (RedisClient client = new RedisClient(host))
            {
                if (client.Get<string>(key) == null)
                {
                    client.Set(key, value);
                }
            }
        }

        private static string ReadData(string host, string key)
        {
            using (RedisClient client = new RedisClient(host))
            {
                return client.Get<string>(key);
            }
        }


        /// <summary>
        /// id_and_action_name contains the id primary key of student and her action to create unique key for each query
        /// </summary>
        /// <param name="id_and_action_name"></param>
        /// <returns></returns>
        public static bool isTheDurationTooShort(string id_and_action_name)
        {
            string lastQuery = RedisService.ReadData("localhost", id_and_action_name);
            if (lastQuery == null)
                RedisService.SaveData("localhost", id_and_action_name, DateTime.Now.ToString());
            lastQuery = RedisService.ReadData("localhost", id_and_action_name);
            TimeSpan duration = new TimeSpan(DateTime.Now.Ticks - DateTime.Parse(lastQuery).Ticks);

            if (duration.Seconds > 10)
            {
                RedisService.SaveData(ConfigurationManager.AppSettings["Minimum_age"].ToString(), id_and_action_name, DateTime.Now.ToString());
                return true;
            }
            return false;
        }


    }
}