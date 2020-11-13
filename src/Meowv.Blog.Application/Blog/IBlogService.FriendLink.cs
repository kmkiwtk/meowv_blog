using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        /// <summary>
        /// 查询友链列表
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync();

        Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinksForAdminAsync();

        Task<ServiceResult> InsertFriendLinkAsync(EditFriendLinkInput input);

        Task<ServiceResult> UpdateFriendLinkAsync(int id, EditFriendLinkInput input);

        Task<ServiceResult> DeleteFriendLinkAsync(int id);
    }
}
