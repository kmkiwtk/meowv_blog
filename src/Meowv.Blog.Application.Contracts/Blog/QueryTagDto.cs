using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// tag查询列表
    /// </summary>
    public class QueryTagDto:TagDto
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
