
using Volo.Abp.Identity;
using Meowv.Blog.Application;
using Volo.Abp.Modularity;


namespace Meowv.Blog.HttpApi
{
    [DependsOn(

        typeof(MeowvBlogApplicationModule),
        typeof(AbpIdentityHttpApiModule)
        )]
    public class MeowvBlogHttpApiModule : AbpModule
    {
        
    }
}
