using Microsoft.Extensions.DependencyInjection;
using UserManager.Domain.Interfaces;
using UserManager.Domain.Services;

namespace UserManager.Domain.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoadServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
