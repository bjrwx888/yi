namespace Yi.Framework.Rbac.Domain.Shared.Dtos;

public class Vue3PureRouterDto
{
    public string Path { get; set; }
    public string Name { get; set; }
    public MetaPureRouterDto Meta { get; set; } = new MetaPureRouterDto();

    public List<Vue3PureRouterDto>? Children { get; set; }
}

public class MetaPureRouterDto
{
    public string Icon { get; set; }
    public string Title { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public string Rank { get; set; }

    public List<string>? Roles { get; set; }

    public List<string>? Auths { get; set; }

    public string? FrameSrc { get; set; }

    public string? FrameLoading { get; set; }

    public bool? KeepAlive { get; set; }

    public bool? showLink { get; set; }
}