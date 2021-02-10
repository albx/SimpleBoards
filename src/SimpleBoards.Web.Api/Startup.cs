using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Persistence;
using SimpleBoards.Persistence.SqlServer;
using System.Text.Json.Serialization;
using SimpleBoards.Web.Api.Services;
using SimpleBoards.Core.ReadModels;
using SimpleBoards.Core.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SimpleBoards.Core;
using System;
using Microsoft.IdentityModel.Tokens;

namespace SimpleBoards.Web.Api
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
            services.AddDbContext<BoardsContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SimpleBoards"),
                    b => b.MigrationsAssembly(typeof(BoardsContextFactory).Assembly.GetName().Name)));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization(options => 
            {
                options.AddPolicy("SimpleBoardsWebApi", policy => 
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "simpleboards.web.api");
                });
            });

            services.AddGrpcClient<Users.UsersClient>(options => 
            {
                options.Address = new Uri("https://localhost:5011");
            });

            services
                .AddScoped<IDatabase, Database>()
                .AddScoped<IBoardCommands, BoardCommands>()
                .AddScoped<IIssueCommands, IssueCommands>()
                .AddScoped<IUserCommands, UserCommands>();

            services
                .AddScoped<BoardsControllerServices>()
                .AddScoped<IssuesControllerServices>()
                .AddScoped<CommentsControllerServices>()
                .AddScoped<UsersControllerServices>();

            services.AddCors(options => 
            {
                options.AddPolicy("Client", policy => 
                {
                    policy.WithOrigins("https://localhost:7001")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            
            services
                .AddControllers()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleBoards.Web.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleBoards.Web.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("Client");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                    .RequireAuthorization("SimpleBoardsWebApi");
            });
        }
    }
}
