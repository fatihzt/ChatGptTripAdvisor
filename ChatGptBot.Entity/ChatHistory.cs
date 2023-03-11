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
        public string Answer { get; set; }
        public string Prompt { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
