using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models.Meta
{
    public class PageResponse<T>
    {
        public int totalPages { get; set; }
        public int totalElements { get; set; }
        public List<T> content { get; set; }
    }
}
