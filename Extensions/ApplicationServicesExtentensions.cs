using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtentensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services , IConfiguration config){
              services.AddScoped<ITokenServices, TokenServices>();
            services.AddDbContext<DataContext>(options =>
            {

                options.UseSqlServer(config.GetConnectionString("Default"));
            });
          
          return services;
        } 
        
    }
}