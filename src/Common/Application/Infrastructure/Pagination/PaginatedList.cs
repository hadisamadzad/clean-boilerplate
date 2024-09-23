namespace Common.Application.Infrastructure.Pagination;

public class PaginatedList<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public IList<T> Data { get; set; }
}