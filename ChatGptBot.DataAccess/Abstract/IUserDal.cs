using ChatGptBot.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.DataAccess.Abstract
{
    public interface IUserDal:IEntityRepository<User>
    {
    }
}
