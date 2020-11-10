using System.Collections.Generic;

namespace Meowv.Blog.Application.Contracts.Blog
{
    /// <summary>
    /// 管理文章传输类
    /// </summary>
    public class QueryPostForAdminDto:QueryPostDto
    {
        /// <summary>
        /// 文章的简要信息
        /// </summary>
        public new IEnumerable<PostBriefForAdminDto> Posts;
    }
}
