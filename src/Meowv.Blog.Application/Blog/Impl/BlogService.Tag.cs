
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.Domain.Blog;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;

namespace Meowv.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync()
        {
            return await _blogCacheService.QueryTagsAsync(async ()=> {
                var result = new ServiceResult<IEnumerable<QueryTagDto>>();

                var list = from tags in await _tagRepository.GetListAsync()
                           join post_tags in await _postTagRepository.GetListAsync()
                           on tags.Id equals post_tags.TagId
                           group tags by new
                           {
                               tags.TagName,
                               tags.DisplayName
                           } into g
                           select new QueryTagDto
                           {
                               TagName = g.Key.TagName,
                               DisplayName = g.Key.DisplayName,
                               Count = g.Count()
                           };

                result.IsSuccess(list);
                return result;
            });
        }

        public async Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>> QueryTagsForAdminAsync()
        {
            var result = new ServiceResult<IEnumerable<QueryTagForAdminDto>>();

            var post_tags =await _postTagRepository.GetListAsync();

            var tags = _tagRepository.GetListAsync().Result.Select(x => new QueryTagForAdminDto
            {
                TagName = x.TagName,
                DisplayName = x.DisplayName,
                Id = x.Id,
                Count = post_tags.Count(p => p.TagId == x.Id)
            });
            result.IsSuccess(tags);
            return result;
        }

        public async Task<ServiceResult> InsertTagAsync(EditTagInput input)
        {
            var result = new ServiceResult();

            var tag = ObjectMapper.Map<EditTagInput, Tag>(input);
            await _tagRepository.InsertAsync(tag);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        public async Task<ServiceResult> UpdateTagAsync(int id,EditTagInput input)
        {
            var result = new ServiceResult();

            var tag = await _tagRepository.GetAsync(id);
            tag.DisplayName = input.DisplayName;
            tag.TagName = input.TagName;

            await _tagRepository.UpdateAsync(tag);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        public async Task<ServiceResult> DeleteTagAsync(int id)
        {
            var result = new ServiceResult();

            var tag = await _tagRepository.FindAsync(id);
            if (tag == null)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Id", id));
                return result;
            }

            await _tagRepository.DeleteAsync(id);
            await _postTagRepository.DeleteAsync(x => x.TagId == id);

            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }
    }
}
