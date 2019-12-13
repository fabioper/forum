using AutoMapper;
using Forum.API.ViewModels;
using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Forum.Infra.Repositories;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Forum.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("ForumConnection"));
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            }).AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://localhost:44394";
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.ApiName = "ForumAPI";
                options.ApiSecret = "secret";
            });

            services.AddControllers()
                    .AddNewtonsoftJson(x => {
                        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            var autoMapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateTopicViewModel, Topic>();
                config.CreateMap<CreateGategoryViewModel, Category>();
                config.CreateMap<CreateSectionViewModel, Section>();
            });

            IMapper mapper = autoMapperConfig.CreateMapper();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ISectionsRepository, SectionsRepository>();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
