using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Common.Helper;

namespace Yi.Framework.Template.Abstract
{
    public abstract class AbstractTemplateProvider : ITemplateProvider
    {
        public  string BuildPath { get; set; }
        public  string TemplatePath { get; set; }
        public  string BakPath { get; set; }
        private  Dictionary<string, string> TemplateDic { get; set; }

        public void Bak() { 
        
        }

        public virtual void Build()
        {
            var templateData = GetTemplateData();
            foreach (var ky in TemplateDic)
            {
                templateData = templateData.Replace(ky.Key, ky.Value);
            }
            File.WriteAllText(BuildPath, templateData);
        }


        protected virtual string GetTemplateData()
        {
            return File.ReadAllText(TemplatePath);
        }

        protected  void AddTemplateDic(string oldStr, string newStr)
        {
            TemplateDic=new Dictionary<string, string>();
            TemplateDic.Add(oldStr, newStr);
        }
    }
}
