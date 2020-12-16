
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Meowv.Blog.HttpApi.Hosting
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("blog",
                builder =>
                {
                    builder.WithOrigins("http://192.168.1.120:8080",
                                        "http://localhost:8080")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });
            services.AddApplication<MeowvBlogHttpApiHostingModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
