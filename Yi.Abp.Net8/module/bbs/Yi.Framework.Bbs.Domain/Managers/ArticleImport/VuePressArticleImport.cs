using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Shared.Model;

namespace Yi.Framework.Bbs.Domain.Managers.ArticleImport
{
    internal class VuePressArticleImport : AbstractArticleImport
    {
        public override List<ArticleEntity> Convert(List<FileObject> fileObjs)
        {
            var logger = LoggerFactory.CreateLogger<VuePressArticleImport>();

            //排序及处理目录名称
            var fileNameHandler = fileObjs.OrderBy(x => x.FileName).Select(x =>
              {
                  var f = new FileObject { Content = x.Content };

                  //除去数字
                  f.FileName = x.FileName.Split('.')[1];
                  return f;
              });


            //处理内容
            var fileContentHandler = fileNameHandler.Select(x =>
             {
                 logger.LogError($"老的值：{x.Content}");
                 var f = new FileObject { FileName = x.FileName };
                 var lines = x.Content.SplitToLines();

                 var num = 0;
                 var startIndex = 0;
                 for (int i = 0; i < lines.Length; i++)
                 {
                     if (lines[i] == "---")
                     {
                         num++;
                         if (num == 2)
                         {
                             startIndex = i;
                             logger.LogError($"startIndex={startIndex}");
                             break;
                         }

                     }

                 }
                 var linesRef = lines.ToList();

                 linesRef.RemoveRange(0, startIndex + 1);
        
                 var result = string.Join(Environment.NewLine, linesRef);
                 logger.LogError($"新的值:{result}");
                 f.Content = result;
                 return f;
             });

            var output = fileContentHandler.Select(x => new ArticleEntity() { Content = x.Content, Name = x.FileName }).ToList();

            return output;
        }
    }
}
