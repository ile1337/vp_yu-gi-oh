using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models.Meta
{
    public enum SortOrder
    {
        ASC = 1,
        DESC = 0,
    }
    public class LazyLoadEvent
    {
        public int first { get; set; }
        public int rows { get; set; }
        public string sortField { get; set; }
        public string[] sortFields { get; set; }
        public SortOrder sortOrder { get; set; }
       
    }
}
