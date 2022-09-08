using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.Framework.Common.Models
{
    public class VueRouterModel : ITreeModel<VueRouterModel>
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int Sort { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public bool Hidden { get; set; }
        public string Redirect { get; set; }
        public string Component { get; set; }
        public bool AlwaysShow { get; set; }
        public Meta Meta { get; set; }
        public IList<VueRouterModel> Children { get; set; }
    }


    public class Meta
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string NoCache { get; set; }
        public string link { get; set; }
    }
}
