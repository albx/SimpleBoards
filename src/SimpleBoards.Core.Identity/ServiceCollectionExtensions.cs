using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleBoards.Core.Identity.Data;
using SimpleBoards.Core.Identity.Models;

namespace SimpleBoards.Core.Identity
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleBoardsIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
                
            return services;
        }
    }
}