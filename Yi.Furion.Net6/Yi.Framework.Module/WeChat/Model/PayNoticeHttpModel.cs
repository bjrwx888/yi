using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Furion;

namespace Yi.Framework.Module.WeChat.Model
{
    /// <summary>
    /// 接收的结果
    /// </summary>
    public class PayNoticeReponse
    {

        /// <summary>
        /// 通知的唯一ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 通知的资源数据类型，支付成功通知为encrypt-resource
        /// </summary>
        public string create_time { get; set; }

        /// <summary>
        /// 通知的资源数据类型，支付成功通知为encrypt-resource
        /// </summary>
        public string event_type { get; set; }

        /// <summary>
        /// 通知的资源数据类型，支付成功通知为encrypt-resource
        /// </summary>
        public string resource_type { get; set; }

        /// <summary>
        /// 回调摘要
        /// </summary>
        public string summary { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public Resource resource { get; set; }
    }

    /// <summary>
    /// 通知的数据
    /// </summary>
    public class Resource
    {
        /// <summary>
        /// 加密算法类型：AEAD_AES_256_GCM
        /// </summary>
        public string algorithm { get; set; }

        /// <summary>
        /// 数据密文
        /// </summary>
        public string ciphertext { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public string associated_data { get; set; }

        /// <summary>
        /// 原始回调类型，为transaction
        /// </summary>
        public string original_type { get; set; }

        /// <summary>
        /// 随机串
        /// </summary>
        public string nonce { get; set; }
    }



    /// <summary>
    /// 解密出来的结果
    /// </summary>
    public class PayNoticeResult
    {
        /// <summary>
        /// 	微信支付系统生成的订单号。
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        /// 订单金额信息
        /// </summary>
        public Amount amount { get; set; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string mchid { get; set; }

        /// <summary>
        /*
         	交易状态，枚举值：
            SUCCESS：支付成功
            REFUND：转入退款
            NOTPAY：未支付
            CLOSED：已关闭
            REVOKED：已撤销（付款码支付）
            USERPAYING：用户支付中（付款码支付）
            PAYERROR：支付失败(其他原因，如银行返回失败)
         */
        /// </summary>
        public string trade_state { get; set; }

        /// <summary>
        /// 银行类型，采用字符串类型的银行标识。
        /// </summary>
        public string bank_type { get; set; }

        /// <summary>
        /// 	优惠功能，享受优惠时返回该字段
        /// </summary>
        public List<PromotionDetail> promotion_detail { get; set; }

        /// <summary>
        /// 支付完成时间
        /// </summary>
        public string success_time { get; set; }

        /// <summary>
        /// 支付者信息
        /// </summary>
        public Payer payer { get; set; }

        /// <summary>
        /// 商户系统内部订单号，只能是数字、大小写字母_-*且在同一个商户号下唯一。
        /// 特殊规则：最小字符长度为6
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string appid { get; set; }

        /// <summary>
        /// 交易状态描述
        /// </summary>
        public string trade_state_desc { get; set; }

        /// <summary>
        /* 交易类型，枚举值：
        JSAPI：公众号支付
        NATIVE：扫码支付
        APP：APP支付
        MICROPAY：付款码支付
        MWEB：H5支付
        FACEPAY：刷脸支付
        */
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /*
         * 银行类型，采用字符串类型的银行标识。
         */
        /// </summary>
        public string attach { get; set; }


        /// <summary>
        /// 支付场景信息描述
        /// </summary>
        public SceneInfo scene_info { get; set; }

    }
    public class Amount
    {
        /// <summary>
        /// 用户支付金额，单位为分。
        /// </summary>
        public int payer_total { get; set; }
        /// <summary>
        /// 	订单总金额，单位为分。
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// CNY：人民币，境内商户号仅支持人民币。
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// 用户支付币种
        /// </summary>
        public string payer_currency { get; set; }
    }

    public class GoodsDetail
    {
        /// <summary>
        /// 商品备注
        /// </summary>
        public string goods_remark { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public int quantity { get; set; }

        /// <summary>
        /// 商品优惠金额
        /// </summary>
        public int discount_amount { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string goods_id { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public int unit_price { get; set; }
    }

    public class PromotionDetail
    {
        /// <summary>
        /// 单品列表	
        /// </summary>
        public int amount { get; set; }

        /// <summary>
        /// 微信出资
        /// </summary>
        public int wechatpay_contribute { get; set; }

        /// <summary>
        /// 券ID
        /// </summary>
        public string coupon_id { get; set; }
        public string scope { get; set; }
        public int merchant_contribute { get; set; }

        /// <summary>
        /// 	优惠名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 其他出资
        /// </summary>
        public int other_contribute { get; set; }

        /// <summary>
        /// currency
        /// </summary>
        public string currency { get; set; }

        /// <summary>
        /// 活动ID
        /// </summary>
        public string stock_id { get; set; }

        /// <summary>
        /// 单品列表	
        /// </summary>
        public List<GoodsDetail> goods_detail { get; set; }
    }

    public class Payer
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public string openid { get; set; }
    }

    public class SceneInfo
    {
        /// <summary>
        /// 商户端设备号
        /// </summary>
        public string device_id { get; set; }
    }


}
