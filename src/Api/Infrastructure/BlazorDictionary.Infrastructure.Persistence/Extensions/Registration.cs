using BlazorDictionary.Api.Application.Interfaces.Repositories;
using BlazorDictionary.Infrastructure.Persistence.Context;
using BlazorDictionary.Infrastructure.Persistence.Repositories;
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

            //Asagidaki kodu sadece ilk defa data olusturmada kullaniyor ve sonrasinda yorum satiri haline getiriyoruz.
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

            services.AddScoped<IUserRepository, UserRepository>(); //Generic repository'i kullanabilmek icin ilgili repository'ler buraya eklenmelidir.
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();


            return services;
        }
    }
}
