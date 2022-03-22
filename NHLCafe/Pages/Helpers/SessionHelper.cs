using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace NHLCafe.Pages.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
        
        public static Nullable<int> Increment(this ISession session, string key)
        {
            var value = session.GetInt32(key);
            value++;
            return value;
        }
        
        public static Nullable<int> Decrement(this ISession session, string key)
        {
            var value = session.GetInt32(key);
            if (value < 1)
                return value;
            value--;
            return value;
        }
    }
}