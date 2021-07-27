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

        public CardDto(string id, string cardId, string type, string name, string description, string subType, int atk, int def, Image img)
        {
            this.id = id;
            this.cardId = cardId;
            this.type = type;
            this.name = name;
            this.description = description;
            this.subType = subType;
            this.atk = atk;
            this.def = def;
            this.img = img;
        }

        public CardDto()
        {
        }
    }
}
