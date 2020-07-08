using AutoMapper;
using Domain.Entity;
using Domain.ViewModels.Comment;
using Domain.ViewModels.FileReference;
using Domain.ViewModels.Post;
using Domain.ViewModels.User;
using Service.Mediator.V1.AccountCase.Register;

namespace Service.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommand, User>();

            
            CreateMap<User, RegisterUserVM>();
            
            CreateMap<User, UserResponse>();


            CreateMap<CommentRequest, Comment>();

            CreateMap<Comment, CommentResponse>();


            CreateMap<FileReference, FileReferenceResponse>();


            CreateMap<PostRequest, Post>();

            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.Files, s => s.Condition(so => so != null))
                .ForMember(dest => dest.Comments, s => s.Condition(so => so != null))
                .ForMember(dest => dest.Username, s => s.Condition(so => so != null))
                .ForMember(dest => dest.Username, options => options.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Files, options => options.MapFrom(src => src.FileReferences))
                ;


            //Mapper.AssertConfigurationIsValid(); //Is OK!
        }
    }
}
