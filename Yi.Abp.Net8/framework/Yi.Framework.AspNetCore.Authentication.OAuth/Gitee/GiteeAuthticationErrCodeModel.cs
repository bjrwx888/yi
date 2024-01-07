using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Yi.Framework.AspNetCore.Authentication.OAuth.Gitee
{
    public class GiteeAuthticationErrCodeModel
    {
        public string error { get; set; }

        public string error_description { get; set; }

        public static void VerifyErrResponse(string content)
        {

                var model =Newtonsoft.Json.JsonConvert.DeserializeObject <GiteeAuthticationErrCodeModel>(content);
                if (model.error != null)
                {

                    throw new Exception($"第三方授权返回错误，错误码：【{model.error}】，错误详情：【{model.error_description}】");
                }
        }

    }

}
