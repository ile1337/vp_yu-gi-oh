using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    public class CardDto
    {
        public string id { get; set; }
        public string cardId { get; set; }
        public string type { get; set; }//enum
        public string name { get; set; }
        public string description { get; set; }
        public string subType { get; set; }//enum
        public int atk { get; set; }
        public int def { get; set; }
        public Image img { get; set; }
        public override string ToString()
        {
            return string.Format("{0} - {1}",name,subType);
        }
    }
}
