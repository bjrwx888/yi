using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Bbs.Domain.Shared.Consts
{
    /// <summary>
    /// 常量定义
    /// </summary>

    public class DiscussConst
    {
        public const string No_Exist = "传入的主题id不存在";

        public const string Privacy = "【私密】您无该主题权限，可联系作者申请开放";

        public const string AgreeNotice = """
                                          <div>
                                           <h3 class="title" style="color: #333; font-size: 18px; margin: 0 0 10px;">🍗 您的主题 [{0}] 被 [{1}] 用户点赞！</h3>
                                                  <p class="link" style="color: #555;font-size: 16px;">
                                                      点击前往主题地址： 
                                                      <a href="/article/{2}" target="_blank" style="color: #007BFF;text-decoration: none;">https://ccnetcore.com/article/{2}</a>
                                                  </p>
                                          </div>
                                          """;
        public const string CommentNotice = """
                                             <div>
                                              <h3 class="title" style="color: #333; font-size: 18px; margin: 0 0 10px;">🍖 您的主题 [{0}] 被 [{1}] 用户评论!</h3>
                                                     <p class="link" style="color: #555;font-size: 16px;">
                                                         评论内容：[{2}]
                                                         点击前往主题地址： 
                                                         <a href="/article/{3}" target="_blank" style="color: #007BFF;text-decoration: none;">https://ccnetcore.com/article/{3}</a>
                                                     </p>
                                             </div>
                                             """;
        public const string CommentNoticeToReply= """
                                                  <div>
                                                   <h3 class="title" style="color: #333; font-size: 18px; margin: 0 0 10px;">🍖 您在主题 [{0}] 的评论被 [{1}] 用户回复!</h3>
                                                          <p class="link" style="color: #555;font-size: 16px;">
                                                              评论内容：[{2}]
                                                              点击前往主题地址： 
                                                              <a href="/article/{3}" target="_blank" style="color: #007BFF;text-decoration: none;">https://ccnetcore.com/article/{3}</a>
                                                          </p>
                                                  </div>
                                                  """;
    }
}
