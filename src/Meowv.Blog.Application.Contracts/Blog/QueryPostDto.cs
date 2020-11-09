
//QueryPostDto.cs
using System.Collections.Generic;

namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 传输查询数据的实体类
    /// </summary>
    public class QueryPostDto
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Posts
        /// </summary>
        public IEnumerable<PostBriefDto> Posts { get; set; }
    }
}
