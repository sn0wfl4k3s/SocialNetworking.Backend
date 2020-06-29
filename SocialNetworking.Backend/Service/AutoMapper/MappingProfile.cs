﻿using AutoMapper;
using Domain.Entity;
using Domain.ViewModels.Comment;
using Domain.ViewModels.FileReference;
using Domain.ViewModels.Post;
using Service.Mediator.V1.AccountCase.Register;

namespace Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, User>();

            CreateMap<User, RegisterUserVM>();

            CreateMap<PostRequest, Post>();

            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Username, options => options.MapFrom(src => src.User.Username))
                ;

            CreateMap<CommentResponse, Comment>();

            CreateMap<Comment, CommentResponse>();


            CreateMap<FileReference, FileReferenceResponse>();


            //Mapper.AssertConfigurationIsValid(); //Is OK!
        }
    }
}
