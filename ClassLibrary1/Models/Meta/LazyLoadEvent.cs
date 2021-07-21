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

        public LazyLoadEvent(int pageNumber, string[] sortFields)
        {
            this.first = (pageNumber - 1) * int.Parse(Properties.PAGE_SIZE);
            this.sortFields = sortFields;

            if(this.sortFields.Length == 0)
            {
                this.sortFields = null;
            }

        }

        public int first { get; set; }
        public int rows { get; set; } = int.Parse(Properties.PAGE_SIZE);
        public string sortField { get; set; }
        public string[] sortFields { get; set; }
        public SortOrder sortOrder { get; set; } = SortOrder.ASC;

}
}
