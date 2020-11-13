using System;
using System.Collections.Generic;
using System.Text;

namespace Meowv.Blog.Application.Contracts.Blog
{
    public class QueryFriendLinkForAdminDto:FriendLinkDto
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
    }
}
