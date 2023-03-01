using ChatGptBot.Business.Manager;
using ChatGptBot.Business.Service;
using ChatGptBot.DataAccess.Abstract;
using ChatGptBot.DataAccess.Concrete;

namespace ChatGptBot.Api.StartUpExtension
{
    public static class ExtensionService
    {
        public static void AddSercices(this IServiceCollection services)
        {
            services.AddScoped<IUserService,UserManager>();
            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IChatHistoryService,ChatHistoryManager>();
            services.AddScoped<IChatHistoryDal,EfChatHistoryDal>();
        }
    }
}
