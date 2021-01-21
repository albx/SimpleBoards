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

                    options.Audience = "simpleboards.web.api";
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            services
                .AddScoped<IDatabase, Database>()
                .AddScoped<IBoardCommands, BoardCommands>()
                .AddScoped<IIssueCommands, IssueCommands>();

            services
                .AddScoped<BoardsControllerServices>()
                .AddScoped<IssuesControllerServices>()
                .AddScoped<CommentsControllerServices>();
            
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

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
