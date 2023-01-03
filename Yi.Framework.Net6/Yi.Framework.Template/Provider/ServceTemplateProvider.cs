using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yi.Framework.Template.Abstract;

namespace Yi.Framework.Template.Provider
{
    public class ServceTemplateProvider : AbstractTemplateProvider
    {
        public ServceTemplateProvider()
        {
            BuildPath = "E:\\Yi\\Yi.Framework.Net6\\Yi.Framework.Template\\Code\\ServiceNewTemplate.txt";
            TemplatePath = "E:\\Yi\\Yi.Framework.Net6\\Yi.Framework.Template\\Template\\ServiceTemplate.txt";
            AddTemplateDic("Yi.Framework", "Yi.TTT");
        }
    }
}
