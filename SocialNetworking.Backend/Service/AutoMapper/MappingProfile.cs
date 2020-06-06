using AutoMapper;
using Domain.Entity;
using Service.Mediator.V1.AccountCase.Register;

namespace Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, User>();

            CreateMap<User, RegisterUserVM>();
            //CreateMap<User, LoginUserVM>();

            //CreateMap<User, UserResponse>();

            //CreateMap<Post, PostResponse>()
            //    .ForMember(dest => dest.Username, options => options.MapFrom(src => src.Author.Username));

            //CreateMap<Comment, CommentResponse>()
            //    .ForMember(dest => dest.Username, options => options.MapFrom(src => src.Author.Username));


            //CreateMap<UserAuthenticated, LoginUserVM>()
            //    .ForMember(dest => dest.Access_token, options => options.MapFrom(src => src.Token))
            //    .ForMember(dest => dest.Expires_in, options => options.MapFrom(src => src.Expires))
            //    .ForMember(dest => dest.Token_type, options => options.MapFrom(src => src.Type));

            //CreateMap<PostRequest, Post>();
            //CreateMap<CommentRequest, Comment>();

            //.ForMember(dest => dest.User.Username, options => options.MapFrom(src => src.Username));

            //Mapper.AssertConfigurationIsValid(); //Is OK!
        }
    }
}
