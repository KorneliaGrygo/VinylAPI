using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VinylAPI.Data;
using AutoMapper;
using VinylAPI.Services;
using VinylAPI.Middleware;
using FluentValidation.AspNetCore;
using FluentValidation;
using VinylAPI.Validators;
using VinylAPI.Models;
using Microsoft.AspNetCore.Identity;
using VinylAPI.Entities;

namespace VinylAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VinylAPIDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("VinylDb"));
            });
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<VinylAPIDbContext>();
            services.AddScoped<VinylSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);

            #region services register
            services.AddScoped<IBandService, BandServices>();
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IMusicAlbumService, MusicAlbumService>();
            services.AddScoped<IAccountService, AccountService>();
            #endregion
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<ErrorHandlingMiddleware>();

            #region validators
            services.AddScoped<IValidator<CreateAlbumDto>, CreateAlbumDtoValidator>(); //renestracna validatora 
            services.AddScoped<IValidator<CreateBandDto>, CreateBandDtoValidator>(); //renestracna validatora 
            services.AddScoped<IValidator<CreateSongDto>, CreateSongDtoValidator>(); //renestracna validatora 
            #endregion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VinylAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VinylSeeder seeder)
        {
            seeder.Seed();
            app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VinylAPI v1");
                    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                });
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
