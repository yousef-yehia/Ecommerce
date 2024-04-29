using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace Api.Helper
{
    public class Pagination<T> where T : class
    {
        public Pagination(IReadOnlyList<T> data, int pageIndex, int pageSize, int count, bool hasNextPage)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
            HasNextPage = hasNextPage;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public bool HasNextPage { get; set; }

        public IReadOnlyList<T> Data { get; set; }

        public static Pagination<T> Paginate(List<T> query, int pageIndex, int pageSize)
        {
            var data = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            var totalCount = data.Count;
            var result = CreateAsync(data, pageIndex, pageSize, totalCount);
            return result;
        }
        public static Pagination<T> CreateAsync(List<T> query, int pageIndex, int pageSize, int count)
        {
            var totalCount = query.Count;
            bool hasNextPage = pageIndex * pageSize < count;

            return new(query, pageIndex, pageSize, totalCount, hasNextPage);
        }
    }
}
