using Meowv.Blog.Application.Contracts;
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
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync(PagingInput input)
        {
            var result = new ServiceResult<PagedList<QueryPostForAdminDto>>();
            var count = await _postRepository.GetCountAsync();
            var list = _postRepository.OrderByDescending(x => x.CreationTime.Year)
                                      .PageByIndex(input.Page, input.Limit)
                                      .Select(x => new PostBriefForAdminDto
                                      {
                                          Id = x.Id,
                                          Title = x.Title,
                                          Year = x.CreationTime.Year,
                                          CreationTime = x.CreationTime.TryToDateTime(),
                                          Url = x.Url
                                      })
                                      .GroupBy(x => x.Year)
                                      .Select(x => new QueryPostForAdminDto
                                      {
                                          Posts = x.ToList(),
                                          Year = x.Key
                                      }).ToList();
            result.IsSuccess(new PagedList<QueryPostForAdminDto>(count.TryToInt(),list));
            return result;
        }
        
        public async Task<ServiceResult> InsertPostAsync(EditPostInput input)
        {
            var result = new ServiceResult();

            var post = ObjectMapper.Map<EditPostInput, Post>(input);
            post.Url = $"{post.CreationTime.ToString(" yyyy MM dd ").Replace(" ", "/")}{post.Url}";
            await _postRepository.InsertAsync(post);

            var tags = await _tagRepository.GetListAsync();

            var newTags = input.Tags
                       .Where(item => !tags.Any(x => x.TagName.Equals(item)))
                       .Select(item => new Tag
                       {
                           TagName = item,
                           DisplayName = item
                       });
            await _tagRepository.BulkInsertAsync(newTags);

            var postTags = input.Tags.Select(item => new PostTag
            {
                PostId = post.Id,
                TagId = _tagRepository.FirstOrDefault(x => x.TagName == item).Id
            });
            await _postTagRepository.BulkInsertAsync(postTags);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }
    }
}
