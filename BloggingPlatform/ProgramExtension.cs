using BusinessLogic;
using Common;
using DataAccess;
using Entities;

namespace BloggingPlatform
{
    public static class ProgramExtension
    {

        public static WebApplicationBuilder ConfigurationBinding(this WebApplicationBuilder Builder)
        {
            Builder.Services.AddControllers();
            Builder.Services.Configuration();
            Builder.Services.AddDbContext<DatabaseContext>();

            var provider = Builder.Services.BuildServiceProvider();
            var configuration = provider.GetService<IConfiguration>();
            AppSettings.Secret = configuration.GetValue<string>("AppSettings:Secret");
            return Builder;
        }

        public static void Configuration(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();


            // var logger = serviceProvider.GetService<ILogger<ExceptionMiddleware>>();
            //services.AddSingleton(typeof(ILogger), logger);

            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<IUserDAL, UserDAL>();

            services.AddScoped<IPostBL, PostBL>();
            services.AddScoped<IPostDAL, PostDAL>();

            services.AddScoped<ICommentBL, CommentBL>();
            services.AddScoped<ICommentDAL, CommentDAL>();

        }
    }
}
