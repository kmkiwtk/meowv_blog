using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    public class PostForAdminDto:PostDto
    {
        /// <summary>
        /// 标签
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}
