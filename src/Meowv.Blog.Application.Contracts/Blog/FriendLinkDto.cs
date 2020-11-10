using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    public class FriendLinkDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 资源地址
        /// </summary>
        public string LinkUrl { get; set; }
    }
}
