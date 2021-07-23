using Middleware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yu_gi_oh
{
    public class Deck
    {
        public string name { get; set; }

        public List<CardDto> cards { get; set; } = new List<CardDto>();


        public override string ToString()
        {
            return string.Format("{0}",name);
        }
    }
}
