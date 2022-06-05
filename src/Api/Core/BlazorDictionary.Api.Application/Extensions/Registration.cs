using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorDictionary.Api.Application.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddApplicationRegistiration(this IServiceCollection services)
        {
            var assm = Assembly.GetExecutingAssembly();

            services.AddMediatR(assm);
            services.AddAutoMapper(assm);
            services.AddValidatorsFromAssembly(assm);

            return services;
        }
    }
}
