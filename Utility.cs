using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imagegram.Functions
{
    public static class Utility
    {
        public static Dictionary<string, int> ReadContinuationToken(string continuationToken)
        {
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(continuationToken));

            return JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        }

        public static string GenerateContinuationToken(Dictionary<string, int> values)
        {
            string json = JsonConvert.SerializeObject(values);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        }
    }
}
