using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.Entity
{
    public class ChatHistory
    {
        public int Id { get; set; }
        public string History { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
