
using Meowv.Blog.Domain.Configurations;
using Meowv.Blog.EntityFrameworkCore;
using Meowv.Blog.HttpApi.Hosting.Filters;
using Meowv.Blog.HttpApi.Hosting.Middleware;
using Meowv.Blog.Swagger;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Meowv.Blog.HttpApi.Hosting
{
    [DependsOn(
       typeof(AbpAspNetCoreMvcModule),
       typeof(AbpAutofacModule),
       typeof(MeowvBlogHttpApiModule),
        typeof(MeowvBlogSwaggerModule),
        typeof(MeowvBlogFrameworkCoreModule)
    )]
    public class MeowvBlogHttpApiHostingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            Configure<MvcOptions>(options =>
            {
                var filterMetadata = options.Filters.FirstOrDefault(x => x is ServiceFilterAttribute attribute && attribute.ServiceType.Equals(typeof(AbpExceptionFilter)));

                // 移除 AbpExceptionFilter
                options.Filters.Remove(filterMetadata);
                //添加自己的异常过滤器
                options.Filters.Add(typeof(MeowvBlogExceptionFilter));
            });


            // 身份验证
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       //设置身份验证参数
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.FromSeconds(30),
                           ValidateIssuerSigningKey = true,
                           ValidAudience = AppSettings.JWT.Domain,
                           ValidIssuer = AppSettings.JWT.Domain,
                           IssuerSigningKey = new SymmetricSecurityKey(AppSettings.JWT.SecurityKey.GetBytes())
                       };
                       options.Events = new JwtBearerEvents
                       {
                           OnChallenge = async context =>
                           {
                               // 跳过默认的处理逻辑，返回下面的模型数据
                               context.HandleResponse();

                               context.Response.ContentType = "application/json;charset=utf-8";
                               context.Response.StatusCode = StatusCodes.Status200OK;

                               var result = new ServiceResult();
                               result.IsFailed("UnAuthorized");

                               await context.Response.WriteAsync(result.ToJson());
                           }
                       };
                   });

            // 认证授权
            context.Services.AddAuthorization();

            // Http请求
            context.Services.AddHttpClient();

           
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            // 环境变量，开发环境
            if (env.IsDevelopment())
            {
                // 生成异常页面
                app.UseDeveloperExceptionPage();
            }

            // 路由
            app.UseRouting();

            // 身份验证
            app.UseAuthentication();

            // 认证授权
            app.UseAuthorization();

            // 异常处理中间件
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // 路由映射
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}