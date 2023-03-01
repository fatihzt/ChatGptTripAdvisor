using ChatGptBot.DataAccess.Abstract;
using ChatGptBot.DataAccess.EntityFrameworkCore;
using ChatGptBot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.DataAccess.Concrete
{
    public class EfChatHistoryDal:EfEntityRepositoryBase<ChatHistory,DataBaseContext>,IChatHistoryDal
    {
    }
}
