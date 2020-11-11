using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Caching
{
    public interface ICacheRemoveService
    {
        Task RemoveAsync(string key, int cursor = 0);
    }
}
