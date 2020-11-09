
//IBlogService.cs
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.ToolKits.Base;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Blog
{
    public partial interface IBlogService
    {
        
        Task<ServiceResult<string>> InsertPostAsync(PostDto dto);

        
        Task<ServiceResult> DeletePostAsync(int id);

        
        Task<ServiceResult<string>> UpdatePostAsync(int id, PostDto dto);

        
        Task<ServiceResult<PostDto>> GetPostAsync(int id);
    }
}