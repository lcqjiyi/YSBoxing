using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YSBoxing.ViewModels
{

    

    public class PagedResultOutput<T> where T : class
    {
        public int totalCount { get; set; }
        public List< T> items { get; set; }
    }
}
