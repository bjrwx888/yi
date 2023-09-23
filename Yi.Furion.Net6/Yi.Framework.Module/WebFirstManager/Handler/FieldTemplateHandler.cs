using System.Text;
using EasyTool;
using Furion.DependencyInjection;

namespace Yi.Framework.Module.WebFirstManager.Handler
{
    public class FieldTemplateHandler : TemplateHandlerBase, ITemplateHandler, ISingleton
    {
        public string Invoker(string str)
        {
            return str.Replace("@field", BuildFields());
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
                var typeStr = EnumUtil.GetDescriptionByValue(field.FieldType);
                var nameStr = field.Name;

                //添加备注
                if (string.IsNullOrEmpty(field.Description))
                {
                    var desStr = "/// <summary>" +
                               @$"///{field.Description}" +
                                 "/// </summary>";
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
