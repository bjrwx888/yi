using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Domain.Shared.Forum.Etos
{
    public class SeeDiscussEventArgs
    {
        public long DiscussId { get; set; }
        public int OldSeeNum { get; set; }
    }
}
