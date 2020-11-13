using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.Domain.Blog;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;

namespace Meowv.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询友链列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync()
        {
            return await _blogCacheService.QueryFriendLinksAsync(async ()=> 
            {
                var result = new ServiceResult<IEnumerable<FriendLinkDto>>();

                var friendLinks = await _friendLinksRepository.GetListAsync();

                var list = ObjectMapper.Map<IEnumerable<FriendLink>, IEnumerable<FriendLinkDto>>(friendLinks);

                result.IsSuccess(list);
                return result;
            });
        }

        public async Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinksForAdminAsync()
        {
            var result = new ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>();

            var friendLinks = await _friendLinksRepository.GetListAsync();

            var dto = ObjectMapper.Map<List<FriendLink>, IEnumerable<QueryFriendLinkForAdminDto>>(friendLinks);


            result.IsSuccess(dto);
            return result;
        }

        public async Task<ServiceResult> InsertFriendLinkAsync(EditFriendLinkInput input)
        {
            var result = new ServiceResult();

            var friendlink = ObjectMapper.Map<EditFriendLinkInput, FriendLink>(input);
            await _friendLinksRepository.InsertAsync(friendlink);

            await _blogCacheService.RemoveAsync(CachePrefix.Blog_FriendLink);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        public async Task<ServiceResult> UpdateFriendLinkAsync(int id, EditFriendLinkInput input)
        {
            var result = new ServiceResult();

            var friendLink = await _friendLinksRepository.GetAsync(id);
            friendLink.LinkUrl = input.LinkUrl;
            friendLink.Title = input.Title;

            await _friendLinksRepository.UpdateAsync(friendLink);

            await _blogCacheService.RemoveAsync(CachePrefix.Blog_FriendLink);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        public async Task<ServiceResult> DeleteFriendLinkAsync(int id)
        {
            var result = new ServiceResult();

            var friendLink = await _friendLinksRepository.FindAsync(id);
            if (friendLink == null)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }

            await _friendLinksRepository.DeleteAsync(id);

            await _blogCacheService.RemoveAsync(CachePrefix.Blog_FriendLink);


            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }
    }
}
