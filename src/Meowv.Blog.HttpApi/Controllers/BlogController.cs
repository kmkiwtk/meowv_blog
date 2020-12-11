
//BlogController.cs
using Meowv.Blog.Application.Blog;
using Meowv.Blog.Application.Contracts;
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Volo.Abp.AspNetCore.Mvc;
using static Meowv.Blog.Domain.Shared.MeowvBlogConsts;

namespace Meowv.Blog.HttpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = Grouping.GroupName_v1)]
    public class BlogController : AbpController
    {
        private readonly IBlogService _blogService;

        #region 前台接口

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ServiceResult<string>> InsertPostAsync([FromBody] PostDto dto)
        {
            return await _blogService.InsertPostAsync(dto);
        }
        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<ServiceResult> TestDeletePostAsync([Required] int id)
        {
            return await _blogService.TestDeletePostAsync(id);
        }

        /// <summary>
        /// 更新博客
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public async Task<ServiceResult<string>> UpdatePostAsync([Required] int id, [FromBody] PostDto dto)
        {
            return await _blogService.UpdatePostAsync(id, dto);
        }

        /// <summary>
        /// 查询博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ServiceResult<PostDto>> GetPostAsync([Required] int id)
        {
            return await _blogService.GetPostAsync(id);
        }

        [HttpGet]
        [Route("posts/tag")]
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByTagAsync([Required]string name)
        {
            return await _blogService.QueryPostsByTagAsync(name);
        }

        /// <summary>
        /// 分类查询文章列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts/category")]
        public async Task<ServiceResult<IEnumerable<QueryPostDto>>> QueryPostsByCategoryAsync([Required]string name)
        {
            return await _blogService.QueryPostsByCategoryAsync(name);
        }

        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("posts")]
        public async Task<ServiceResult<PagedList<QueryPostDto>>> QueryPostsAsync([FromQuery] PagingInput input)
        {
            return await _blogService.QueryPostsAsync(input);
        }


        /// <summary>
        /// 根据URL获取文章详情
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("post")]
        public async Task<ServiceResult<PostDetailDto>> GetPostDetailAsync(string url)
        {
            return await _blogService.GetPostDetailAsync(url);
        }

        /// <summary>
        ///查询分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public async Task<ServiceResult<IEnumerable<QueryCategoryDto>>> QueryCategoriesAsync()
        {
            return await _blogService.QueryCategoriesAsync();
        }

        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("tags")]
        public async Task<ServiceResult<IEnumerable<QueryTagDto>>> QueryTagsAsync()
        {
            return await _blogService.QueryTagsAsync();
        }

        /// <summary>
        /// 查询友链列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("friendlinks")]
        public async Task<ServiceResult<IEnumerable<FriendLinkDto>>> QueryFriendLinksAsync()
        {
            return await _blogService.QueryFriendLinksAsync();
        }

        #endregion



        #region 后台接口


        #region  文章
        /// <summary>
        /// 分页查询文章列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("admin/posts")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<PagedList<QueryPostForAdminDto>>> QueryPostsForAdminAsync([FromQuery]PagingInput input)
        {
            return await _blogService.QueryPostsForAdminAsync(input);
        }


        /// <summary>
        /// 编辑提交新的文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("post")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertPostAsync([FromBody]EditPostInput input)
        {
            return await _blogService.InsertPostAsync(input);
        }


        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("post")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpdatePostAsync([Required] int id, [FromBody] EditPostInput input)
        {
            return await _blogService.UpdatePostAsync(id, input);
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("post")]
        [ApiExplorerSettings(GroupName =Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeletePostAsync ([Required] int id)
        {
            return await _blogService.DeletePostAsync(id);
        }

        /// <summary>
        /// 获取编辑文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("admin/post")]
        [ApiExplorerSettings(GroupName =Grouping.GroupName_v2)]
        public async Task<ServiceResult<PostForAdminDto>> GetPostForAdminAsync(int id)
        {
            return await _blogService.GetPostForAdminAsync(id);
        }

        #endregion


        #region  分类


        /// <summary>
        /// 获取文章分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("admin/categories")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<IEnumerable<QueryCategoryForAdminDto>>> QueryCategoriesForAdminAsync ()
        {
            return await _blogService.QueryCategoriesForAdminAsync();
        }

        /// <summary>
        /// 添加文章分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("category")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertCategoryAsync( [FromBody]EditCategoryInput input)
        {
            return await _blogService.InsertCategoryAsync( input);
        }

        /// <summary>
        /// 更新文章分类
        /// </summary>
        /// <param name="id">分类id</param>
        /// <param name="input">新的分类</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("category")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpdateCategoryAsync([Required]int id,[FromBody] EditCategoryInput input)
        {
            return await _blogService.UpdateCategoryAsync(id, input);
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("category")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeleteCategoryAsync([Required]int id)
        {
            return await _blogService.DeleteCategoryAsync(id);
        }

        #endregion


        #region  标签

        /// <summary>
        /// 查询文章标签列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("admin/tags")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<IEnumerable<QueryTagForAdminDto>>> QueryTagsForAdminAsync()
        {
            return await _blogService.QueryTagsForAdminAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("tag")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertTagAsync([FromBody]EditTagInput input)
        {
            return await _blogService.InsertTagAsync(input);
        }

        /// <summary>
        /// 更新文章标签
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("tag")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpdateTagAsync(int id,EditTagInput input)
        {
            return await _blogService.UpdateTagAsync(id, input);
        }

        /// <summary>
        /// 删除文章标签
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("tag")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeleteTagAsync(int id)
        {
            return await _blogService.DeleteTagAsync(id);
        }

        #endregion


        #region  友链
        /// <summary>
        /// 查询友链
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("admin/friendlinks")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult<IEnumerable<QueryFriendLinkForAdminDto>>> QueryFriendLinksForAdminAsync()
        {
            return await _blogService.QueryFriendLinksForAdminAsync();
        }

        /// <summary>
        /// 提交友链
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> InsertFriendLinkAsync(EditFriendLinkInput input)
        {
            return await _blogService.InsertFriendLinkAsync(input);
        }

        /// <summary>
        /// 更新友链
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> UpdateFriendLinkAsync(int id,EditFriendLinkInput input)
        {
            return await _blogService.UpdateFriendLinkAsync(id,input);
        }

        /// <summary>
        /// 删除友链
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("friendlink")]
        [ApiExplorerSettings(GroupName = Grouping.GroupName_v2)]
        public async Task<ServiceResult> DeleteFriendLinkAsync(int id )
        {
            return await _blogService.DeleteFriendLinkAsync(id);
        }

        #endregion

        #endregion

    }
}
