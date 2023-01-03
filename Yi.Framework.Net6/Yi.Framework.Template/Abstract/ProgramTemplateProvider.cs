using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Const;

namespace Yi.Framework.Template.Abstract
{

    public abstract class ProgramTemplateProvider : AbstractTemplateProvider
    {
        public ProgramTemplateProvider(string modelName, string entityName)
        {
            ModelName = modelName;
            EntityName = entityName;
            base.AddTemplateDic(TemplateConst.EntityName, EntityName);
            base.AddTemplateDic(TemplateConst.ModelName, ModelName);
        }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// 重写构建路径，替换实体名称与模块名称
        /// </summary>
        public override string? BuildPath
        {
            get => base.BuildPath;
            set
            {
                value = value!.Replace(TemplateConst.EntityName, EntityName);
                value = value.Replace(TemplateConst.ModelName, ModelName);
                base.BuildPath = value;
            }
        }

        public override void Build()
        {
            if (BuildPath is null)
            {
                throw new ArgumentNullException(nameof(BuildPath));
            }
            var templateData = GetTemplateData();
            foreach (var ky in TemplateDic)
            {
                templateData = templateData.Replace(ky.Key, ky.Value);
            }
            if (!Directory.Exists(Path.GetDirectoryName(BuildPath)))
            { 
                Directory.CreateDirectory(Path.GetDirectoryName(BuildPath)!);
            }
            File.WriteAllText(BuildPath, templateData);
        }

        public override void Bak()
        {
            throw new NotImplementedException();
        }
    }
}
