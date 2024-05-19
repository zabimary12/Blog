using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ArticleDto, Article>().ReverseMap();
            CreateMap<CommentDto, Comment>().ReverseMap();
            CreateMap<TagDto, Tag>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }

        public static MapperConfiguration InitializeAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(conf => conf.AddProfile(new AutoMapperProfile()));

            return mapperConfiguration;
        }
    }
}