//PostForPagedDto.cs
namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 翻页用的数据
    /// </summary>
    public class PostForPagedDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string Url { get; set; }
    }
}