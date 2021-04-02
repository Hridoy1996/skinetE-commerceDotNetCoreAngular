using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Core.Interfaces;
using Infrastructure.Data;
using API.Helpers;
using StackExchange.Redis;
using API.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using MongoDbGenericRepository;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Core.Entities.Identity;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _configuration; 
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddSingleton<IConnectionMultiplexer>(c =>
           {
               var configuration = c.GetRequiredService<IConfiguration>()["Redis"];
               return ConnectionMultiplexer.Connect(configuration);

           });
            services.AddApplicationServices();
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            */
            services.AddSingleton<IMongoClient, MongoClient>(s =>
           {
               var connectionString = s.GetRequiredService<IConfiguration>()["MongoUri"];
               return new MongoClient(connectionString);
           });
            var mongoDbContext = new MongoDbContext("mongodb://localhost:27017", "skinet_db");
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<IMongoDbContext>(mongoDbContext)
                .AddDefaultTokenProviders();
            services.AddMvc();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidateIssuer = true, 
                        ValidateAudience = false
                    };
                    
                });
            services.AddAuthorization();


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
