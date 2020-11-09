using Meowv.Blog.Application.Contracts;
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Caching.Blog
{
    public partial interface IBlogCacheService
    {
        Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync(Func<Task<ServiceResult<IEnumerable<QueryCategoryDto>>>> factory);
    }
}
