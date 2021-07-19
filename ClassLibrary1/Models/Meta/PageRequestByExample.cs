using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models.Meta
{
    public class PageRequestByExample<DTO>
    {
        public PageRequestByExample(DTO example, int pageNumber, string[] sortFields) {
            this.example = example;
            lazyLoadEvent = new LazyLoadEvent(pageNumber, sortFields);
        }

        public DTO example { get; set; }
        public LazyLoadEvent lazyLoadEvent { get; set; }
    }
   
}
