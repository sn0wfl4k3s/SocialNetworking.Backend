using AutoMapper;
using Core.Domain;
using Core.Service;
using Core.Service.Requests;
using Core.Service.Validations;
using CrossCutting.Account;
using CrossCutting.Authentication;
using CrossCutting.Configuration;
using CrossCutting.File;
using CrossCutting.Security;
using Domain.Entity;
using Domain.ViewModels.Comment;
using Domain.ViewModels.Post;
using FluentValidation;
using InfraData.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Mediator.V1.AccountCase.Login;
using Service.Mediator.V1.AccountCase.Register;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MyNetwork.WebApi.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            var options = provider.GetService<IOptions<JwtSettings>>();

            var key = Encoding.ASCII.GetBytes(options.Value.SigningKey);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddRepositorys(this IServiceCollection services)
        {
            services.AddTransient<IEntityRepository<User>, UserRepository>();
            services.AddTransient<IEntityRepository<Post>, PostRepository>();
            services.AddTransient<IEntityRepository<Comment>, CommentRepository>();
            services.AddTransient<IEntityRepository<FileReference>, FileReferenceRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptService, CryptService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFileService, FileService>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var assemblies = new List<Assembly>
            {
                AppDomain.CurrentDomain.Load("Service"),
                AppDomain.CurrentDomain.Load("APIRest")
            };

            services.AddAutoMapper(assemblies);

            return services;
        }

        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Service");

            services.AddMediatR(assembly);

            return services;
        }

        public static IServiceCollection AddMediatorWithValidations(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Service");

            services.AddMediatR(assembly);

            AssemblyScanner
                .FindValidatorsInAssembly(assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            // Account
            services.AddValidation<LoginUserCommand, LoginUserVM>();
            services.AddValidation<RegisterUserCommand, RegisterUserVM>();

            // Comment
            services.AddValidation<CriarRequest<CommentRequest, CommentResponse>, CommentResponse>();
            services.AddValidation<AtualizarRequest<CommentRequest, CommentResponse>, CommentResponse>();
            services.AddValidation<RemoverRequest<CommentRequest, CommentResponse>, CommentResponse>();

            // Post
            services.AddValidation<CriarRequest<PostRequest, PostResponse>, PostResponse>();
            services.AddValidation<AtualizarRequest<PostRequest, PostResponse>, PostResponse>();
            services.AddValidation<RemoverRequest<PostRequest, PostResponse>, PostResponse>();

            return services;
        }

        private static IServiceCollection AddValidation<TInput, TOutput>(this IServiceCollection services)
            where TInput : IRequest<Response<TOutput>>
            where TOutput : class
        {
            services.AddScoped(
                typeof(IPipelineBehavior<TInput, Response<TOutput>>),
                typeof(FailFastRequestBehavior<TInput, Response<TOutput>, TOutput>));

            return services;
        }

        public static IServiceCollection AddVersionamentoApi(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }

        public static IServiceCollection AddAuthSwagger(this IServiceCollection services, string apiName)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description = "Copie 'Bearer ' + token'",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder AddSwaggerVersionado(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }

                options.DocExpansion(DocExpansion.List);
            });

            return app;
        }
    }
}