using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 按种类搜索的列表
    /// </summary>
    public class QueryCategoryDto : CategoryDto
    {
        /// <summary>
        /// 文章数量
        /// </summary>
        public int Count{ get; set; }
    }
}
