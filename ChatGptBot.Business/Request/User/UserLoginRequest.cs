using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.Business.Request.User
{
    public class UserLoginRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
