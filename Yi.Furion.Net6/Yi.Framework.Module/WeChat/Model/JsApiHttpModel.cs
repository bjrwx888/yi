using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WeChat.Model
{
    public class JsApiInput {

        /// <summary>
        /// 商品描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }


        /// <summary>
        /// 订单金额
        /// </summary>
        public AmountItemRequest amount { get; set; }


        /// <summary>
        /// 支付者
        /// </summary>
        public PayerItemRequest payer { get; set; }
    }

    public class JsApiRequest
    {
        /// <summary>
        /// 应用id
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 商户id
        /// </summary>
        public string mchid { get; set; }


        /// <summary>
        /// 商品描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }


        /// <summary>
        /// 回调通知地址
        /// </summary>
        public string notify_url { get; set; }


        /// <summary>
        /// 订单金额
        /// </summary>
        public AmountItemRequest amount { get; set; }


        /// <summary>
        /// 支付者
        /// </summary>
        public PayerItemRequest payer { get; set; }
    }

    public class AmountItemRequest
    {
        /// <summary>
        /// 总金额
        /// </summary>
        public int total { get; set; }
    }


    public class PayerItemRequest
    {
        public string openid { get; set; }
    }


    public class JsApiResponse
    {
        /// <summary>
        /// 预支付id
        /// </summary>
        public string prepay_id { get; set; }
    }
}
