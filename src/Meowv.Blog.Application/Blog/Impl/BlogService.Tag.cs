﻿
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
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
    }
}
