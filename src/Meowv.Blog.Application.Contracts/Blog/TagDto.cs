
//TagDto.cs
namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 标签列表的实体类
    /// </summary>
    public class TagDto
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
