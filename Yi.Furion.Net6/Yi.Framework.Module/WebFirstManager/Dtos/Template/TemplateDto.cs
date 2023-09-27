using Yi.Framework.Infrastructure.Ddd.Dtos.Abstract;

namespace Yi.Framework.Module.WebFirstManager.Dtos.Template
{
    public class TemplateDto : IEntityDto<long>
    {
        public long Id { get; set; }

        /// <summary>
        /// 模板字符串
        /// </summary>
        public string TemplateStr { get; set; } = string.Empty;

        /// <summary>
        /// 生成路径
        /// </summary>
        public string BuildPath { get; set; }


        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remarks { get; set; }
    }
}
