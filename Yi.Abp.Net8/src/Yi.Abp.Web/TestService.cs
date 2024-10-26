using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Services;

namespace Yi.Abp.Application.Services
{
    /// <summary>
    /// 常用魔改及扩展示例
    /// </summary>
    public class TestService : ApplicationService
    {
        [HttpGet("hello-world/string")]
        public async Task<string> GetHelloWorld1(string? name)
        {
            return "1";
        }
        [HttpGet("hello-world/dto")]
        public async Task<CommentGetOutputDto> GetHelloWorld2(string? name)
        {
            return new CommentGetOutputDto();
        }
    }
}

public class CommentGetOutputDto
{

    public DateTime? CreateTime { get; set; }
    public string Content { get; set; }

    public Guid DiscussId { get; set; }
    
    /// <summary>
    /// 根节点的评论id
    /// </summary>
    public Guid RootId { get; set; }

    /// <summary>
    /// 被回复的CommentId
    /// </summary>
    public Guid ParentId { get; set; }

}
