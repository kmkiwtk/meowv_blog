
//CategoryDto.cs
namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 种类列表实体类
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}