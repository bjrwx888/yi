using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Module.WeChat.Abstract;

namespace Yi.Framework.Module.WeChat.Model
{
    public class UniformMessageRequest
    {
        /// <summary>
        ///用户openid，可以是小程序的openid，也可以是mp_template_msg.appid对应的公众号的openid
        /// </summary>
        public string touser { get; set; }


        /// <summary>
        /// 小程序消息模板
        /// </summary>
        public WeappTemplateMsg weapp_template_msg { get; set; } = new WeappTemplateMsg();

        /// <summary>
        /// 公众号模板
        /// </summary>
        public MpTemplateMsg mp_template_msg { get; set; } = new MpTemplateMsg();
    }


    public class UniformMessageInput
    {

        /// <summary>
        ///用户openid，可以是小程序的openid，也可以是mp_template_msg.appid对应的公众号的openid
        /// </summary>
        public string touser { get; set; }


        /// <summary>
        /// 小程序消息模板
        /// </summary>
        public WeappTemplateMsg? weapp_template_msg { get; set; }

        /// <summary>
        /// 公众号模板
        /// </summary>
        public MpTemplateMsg? mp_template_msg { get; set; }
    }

    public class UniformMessageResponse : IErrorObjct
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
    }

    /// <summary>
    /// 小程序消息
    /// </summary>
    public class WeappTemplateMsg
    {

        /// <summary>
        /// 模板id
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 小程序页面
        /// </summary>
        public string page { get; set; }

        /// <summary>
        /// 小程序模板消息formid
        /// </summary>
        public string form_id { get; set; }

        /// <summary>
        /// 小程序模板放大关键词
        /// </summary>
        public string emphasis_keyword { get; set; }

        /// <summary>
        /// 模板数据
        /// </summary>
        public string data { get; set; }
    }


    /// <summary>
    /// 公众号消息通知
    /// </summary>
    public class MpTemplateMsg
    {
        /// <summary>
        /// 公众号appid，要求与小程序有绑定且同主体
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 公众号模板id
        /// </summary>
        public string template_id { get; set; }


        /// <summary>
        ///公众号模板消息所要跳转的url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 公众号模板消息所要跳转的小程序，小程序的必须与公众号具有绑定关系
        /// </summary>
        public Miniprogram miniprogram { get; set; }

        /// <summary>
        /// 公众号模板消息的数据
        /// </summary>
        public Dictionary<string, keyValueItem> data { get; set; }


    }

    /// <summary>
    /// 小程序跳转
    /// </summary>
    public class Miniprogram
    {

        public string appid { get; set; }
        public string pagepath { get; set; }
    }



    public class keyValueItem
    {
        public string value { get; set; }
        public string color { get; set; }


    }
}
