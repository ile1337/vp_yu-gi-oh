using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Models
{
    public class AccountDto
    {
        public AccountDto(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

    }
}
