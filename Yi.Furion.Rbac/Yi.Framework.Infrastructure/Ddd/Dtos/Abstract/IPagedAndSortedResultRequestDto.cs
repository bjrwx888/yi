

using Yi.Framework.Infrastructure.Enums;

namespace Yi.Framework.Infrastructure.Ddd.Dtos.Abstract
{
    public interface IPagedAndSortedResultRequestDto
    {
        int PageNum { get; set; }
        int PageSize { get; set; }
        string? SortBy { get; set; }

        OrderByEnum SortType { get; set; }
    }
}
