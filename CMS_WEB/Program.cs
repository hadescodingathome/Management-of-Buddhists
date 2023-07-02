using CMS_Core.Business;
using CMS_Infrastructure.AppContext;
using CMS_Infrastructure.Business;
using CMS_Infrastructure.IBusiness;
using CMS_WebDesignCore.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace CMS_WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Đọc các giá trị từ appsettings.json

            //var connectionString = configuration.GetConnectionString("local");
            var secretKey = configuration.GetValue<string>("AuthSettings:Key");


            //Accept CORS
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:8080", "http://www.contoso.com")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                                  });
            });


            //Fix JSON circular reference
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            //Add DbContext
            builder.Services.AddDbContext<AppDbContext>();

            //Add Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 5;

            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            // JSON Web Token
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate Token Issuer
                    ValidateIssuer = false,

                    // Validate Token Audience
                    ValidateAudience = false,

                    // Validate Token Signature
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Configure Authorization Policies
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("ADMINANDMEMBER", policy =>
            //    {
            //        policy.RequireClaim(ClaimTypes.Role, new[] { "ADMIN", "MEMBER" });
            //    });
            //    options.AddPolicy("MEMBER", policy =>
            //    {
            //        policy.RequireClaim(ClaimTypes.Role, "MEMBER");
            //    });
            //    options.AddPolicy("ADMIN", policy =>
            //    {
            //        policy.RequireClaim(ClaimTypes.Role, "ADMIN");
            //    });
            //});

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Documentation", Version = "v1" });

                // Configure JWT authentication in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Add services with DI
            builder.Services.AddScoped<IForm, FormService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddTransient<IMailService, MailService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation");
                });
                app.UseCors(MyAllowSpecificOrigins);
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
