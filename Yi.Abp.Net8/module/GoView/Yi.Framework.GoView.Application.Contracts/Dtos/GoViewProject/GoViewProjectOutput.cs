using Newtonsoft.Json;

namespace Yi.Framework.GoView.Application.Contracts.Dtos
{
    /// <summary>
    /// GoView 项目 Item
    /// </summary>
    public class GoViewProjectItemOutput
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// 项目状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 预览图片url
        /// </summary>
        public string IndexImage { get; set; } = string.Empty;

        /// <summary>
        /// 背景图片url
        /// </summary>
        public string BackGroundImage { get; set; } = string.Empty;

        /// <summary>
        /// 创建者Id
        /// </summary>
        public long? CreateUserId { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;
    }

    /// <summary>
    /// GoView 项目详情
    /// </summary>
    public class GoViewProjectDetailOutput : GoViewProjectItemOutput
    {
        /// <summary>
        /// 项目内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
    }

    /// <summary>
    /// GoView 新增项目输出
    /// </summary>
    public class GoViewProjectCreateOutput
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string Id { get; set; }
    }

    /// <summary>
    /// GoView 上传项目输出
    /// </summary>
    public class GoViewProjectUploadOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 仓储名称
        /// </summary>
        public string BucketName { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public long? CreateUserId { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小KB
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 文件后缀
        /// </summary>
        public string FileSuffix { get; set; } = string.Empty;

        /// <summary>
        /// 文件 Url
        /// </summary>
        [JsonProperty("fileurl")]
        public string FileUrl { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        public long? UpdateUserId { get; set; }
    }
}

