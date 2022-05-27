using BlazorDictionary.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorDictionary.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BlazorDictionaryContext>(conf =>
            {
                var connStr = configuration["BlazorDictionaryDbConnectionString"].ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure(); //Veritabanina baglanirken bir hata alirsak diye
                });
            });

            return services;
        }
    }
}
