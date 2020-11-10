using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    public class PostBriefForAdminDto:PostBriefDto
    {
        /// <summary>
        /// 文章的ID
        /// </summary>
        public int Id { get; set; }
    }
}
