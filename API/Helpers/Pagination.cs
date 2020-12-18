using System.Collections.Generic;
using Core.Specifications;

namespace API.Helpers
{
    public class Pagination<T> where T: class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        
        //represents count of items after filters have been applied
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

    }
}