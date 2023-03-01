using ChatGptBot.Business.Service;
using ChatGptBot.DataAccess.Abstract;
using ChatGptBot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.Business.Manager
{
    public class ChatHistoryManager : IChatHistoryService
    {
        private readonly IChatHistoryDal _chatHistoryDal;
        public ChatHistoryManager(IChatHistoryDal chatHistoryDal)
        {
            _chatHistoryDal= chatHistoryDal;
        }
        public int Add(ChatHistory entity)
        {
            return _chatHistoryDal.Add(entity);
        }

        public bool Delete(ChatHistory entity)
        {
            return _chatHistoryDal.Delete(entity);
        }

        public ChatHistory Get(Expression<Func<ChatHistory, bool>> filter = null)
        {
            return _chatHistoryDal.Get(filter);
        }

        public List<ChatHistory> GetAll(Expression<Func<ChatHistory, bool>> filter = null)
        {
            return _chatHistoryDal.GetAll(filter);
        }

        public bool Update(ChatHistory entity)
        {
            return _chatHistoryDal.Update(entity);
        }
    }
}
