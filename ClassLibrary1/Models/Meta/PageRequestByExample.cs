using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models.Meta
{
    public class PageRequestByExample<DTO>
    {
        public DTO example { get; set; }
        public LazyLoadEvent lazyLoadEvent { get; set; }
    }
   
}
