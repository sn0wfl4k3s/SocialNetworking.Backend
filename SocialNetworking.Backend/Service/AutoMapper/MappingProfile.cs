using AutoMapper;
using Domain.Entity;
using Domain.ViewModels.Comment;
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

            //CreateMap<User, LoginUserVM>();s

            //CreateMap<User, UserResponse>();

            CreateMap<PostRequest, Post>();

            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Username, options => options.MapFrom(src => src.Author.Username))
                ;

            CreateMap<CommentResponse, Comment>();

            CreateMap<Comment, CommentResponse>()
                ;


            //CreateMap<UserAuthenticated, LoginUserVM>()
            //    .ForMember(dest => dest.Access_token, options => options.MapFrom(src => src.Token))
            //    .ForMember(dest => dest.Expires_in, options => options.MapFrom(src => src.Expires))
            //    .ForMember(dest => dest.Token_type, options => options.MapFrom(src => src.Type));



            //Mapper.AssertConfigurationIsValid(); //Is OK!
        }
    }
}
