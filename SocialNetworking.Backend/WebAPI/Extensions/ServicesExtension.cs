using AutoMapper;
using Core.Domain;
using Domain.Entity;
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
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using WebAPI.Configuration;

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

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddTransient<ICryptService, CryptService>();

            return services;
        }

        public static IServiceCollection AddLibrarys(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Service");
            services.AddMediatR(assembly);

            var assemblies = new List<Assembly>
            {
                AppDomain.CurrentDomain.Load("Service"),
                AppDomain.CurrentDomain.Load("WebApi")
            };

            services.AddAutoMapper(assemblies);

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