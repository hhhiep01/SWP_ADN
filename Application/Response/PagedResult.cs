using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public PagedResult(IEnumerable<T> items, int total, int pageIndex, int pageSize)
        {
            Items = items;
            Total = total;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
