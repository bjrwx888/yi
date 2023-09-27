using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Furion.DependencyInjection;
using Yi.Framework.Module.WebFirstManager.Enums;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public class FieldTemplateHandler : TemplateHandlerBase, ITemplateHandler, ISingleton
    {
        public HandledTemplate Invoker(string str,string path)
        {
           var output= new HandledTemplate();
            output.TemplateStr = str.Replace("@field", BuildFields());
            output.BuildPath = path;
            return output;
        }


        /// <summary>
        /// 生成Fields
        /// </summary>
        /// <returns></returns>
        public string BuildFields()
        {
            StringBuilder fieldStrs = new StringBuilder();


            foreach (var field in Table.Fields)
            {
                var typeStr = typeof(FieldTypeEnum).GetFields().Where(x=> x.Name== field.FieldType.ToString())?.FirstOrDefault().GetCustomAttribute<DisplayAttribute>().Name;

                if (typeStr is null)
                {
                    continue;
                }
                var nameStr = field.Name;

                //添加备注
                if (!string.IsNullOrEmpty(field.Description))
                {
                    var desStr = "/// <summary>\n" +
                                $"///{field.Description}\n" +
                                 "/// </summary>\n";
                    fieldStrs.AppendLine(desStr);
                }

                //添加长度
                if (field.Length != 0)
                {
                    var lengthStr = $"[SugarColumn(Length ={field.Length})]";
                    fieldStrs.AppendLine(lengthStr);
                }

                //添加字段
                var fieldStr = $"public {typeStr} {nameStr} {{ get; set; }}";

                fieldStrs.AppendLine(fieldStr);
            }

            return fieldStrs.ToString();
        }
    }
}
