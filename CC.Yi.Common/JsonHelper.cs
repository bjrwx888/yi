using System;

namespace CC.Yi.Common
{
    public static class JsonHelper
    {
        public static string JsonToString(object q)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(q);
        }
    }
}
