

//HelloWorldService.cs
namespace Meowv.Blog.Application.HelloWorld.Impl
{
    public class HelloWorldService : MeowvBlogApplicationServiceBase, IHelloWorldService
    {
        public string HelloWorld()
        {
            return "Hello World";
        }
    }
}
