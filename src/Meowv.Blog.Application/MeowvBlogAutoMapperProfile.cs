
//MeowvBlogAutoMapperProfile.cs
using AutoMapper;
using Meowv.Blog.Application.Contracts.Blog;
using Meowv.Blog.Domain.Blog;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Meowv.Blog.Application
{
    public class MeowvBlogAutoMapperProfile : Profile
    {
        /// <summary>
        /// 此处添加map的规则
        /// </summary>
        public MeowvBlogAutoMapperProfile()
        {
            CreateMap<Post, PostDto>();

            CreateMap<PostDto, Post>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<FriendLink, FriendLinkDto>();
        }
    }
}