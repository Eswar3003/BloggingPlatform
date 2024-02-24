using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PagedList<T> : List<T>
    {

        public int CurrentPage { get; init; }

        public int TotalPages { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public OutputParameters Pagination
        {
            get
            {
                return new OutputParameters
                {
                    CurrentPage = CurrentPage,
                    TotalPages = TotalPages,
                    PageSize = PageSize,
                    TotalCount = TotalCount,
                    HasPrevious = HasPrevious,
                    HasNext = HasNext
                };
            }
        }
    }
}
