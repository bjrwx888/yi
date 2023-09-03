using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Module.WeChat.Enums
{
    public enum PayTradeStateEnum
    {
        SUCCESS,//：支付成功
        REFUND,//：转入退款
        NOTPAY,//：未支付
        CLOSED,//：已关闭
        REVOKED,//：已撤销（付款码支付）
        USERPAYING,//：用户支付中（付款码支付）
        PAYERROR,//：支付失败(其他原因，如银行返回失败)
    }
}
