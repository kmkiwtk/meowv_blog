

//BlogService.Category.cs
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.Domain.Blog;
using Meowv.Blog.ToolKits.Base;
using Meowv.Blog.ToolKits.Extensions;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;

namespace Meowv.Blog.Application.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// 查询分类列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync()
        {
            return await _blogCacheService.QueryCategoriesAsync(async () =>
            {
                var result = new ServiceResult<IEnumerable<QueryCategoryDto>>();

                var list = from category in await _categoryRepository.GetListAsync()
                           join posts in await _postRepository.GetListAsync()
                           on category.Id equals posts.CategoryId
                           group category by new
                           {
                               category.CategoryName,
                               category.DisplayName
                           } into g
                           select new QueryCategoryDto
                           {
                               CategoryName = g.Key.CategoryName,
                               DisplayName = g.Key.DisplayName,
                               Count = g.Count()
                           };

                result.IsSuccess(list);
                return result;
            });
        }

        /// <summary>
        /// 按分类查询文章列表
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdminAsync()
        {
            var result = new ServiceResult<IEnumerable<QueryCategoryForAdminDto>>();
            var posts = await _postRepository.GetListAsync();
            var categories =  _categoryRepository.GetListAsync()
                                                 .Result
                                                 .Select(x => new QueryCategoryForAdminDto
                                                 {
                                                     Id = x.Id,
                                                     CategoryName = x.CategoryName,
                                                     DisplayName = x.DisplayName,
                                                     Count = posts.Count(p => p.CategoryId == x.Id)
                                                 });
            result.IsSuccess(categories);
            return result;
        }

        /// <summary>
        /// 插入新的文章分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> InsertCategoryAsync(EditCategoryInput input)
        {
            var result=new ServiceResult();

            var category = ObjectMapper.Map<EditCategoryInput, Category>(input);

            await _categoryRepository.InsertAsync(category);

            result.IsSuccess(ResponseText.INSERT_SUCCESS);
            return result;
        }

        /// <summary>
        /// 更新文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateCategoryAsync(int id,EditCategoryInput input )
        {
            var result = new ServiceResult();

            var category =await _categoryRepository.GetAsync(id);

            if(category==null)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Category",id));
                return result;
            }

            category.CategoryName = input.CategoryName;
            category.DisplayName = input.DisplayName;

            await _categoryRepository.UpdateAsync(category);

            result.IsSuccess(ResponseText.UPDATE_SUCCESS);
            return result;
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteCategoryAsync(int id)
        {
            var result = new ServiceResult();

            var category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                result.IsFailed(ResponseText.WHAT_NOT_EXIST.FormatWith("Category", id));
                return result;
            }

            await _categoryRepository.DeleteAsync(id);

            result.IsSuccess(ResponseText.DELETE_SUCCESS);
            return result;
        }
    }
}
