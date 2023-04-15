using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yi.BBS.Application.Contracts.Forum.Dtos
{
    /// <summary>
    /// Label输入创建对象
    /// </summary>
    public class MyTypeCreateInputVo
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }
    }
}