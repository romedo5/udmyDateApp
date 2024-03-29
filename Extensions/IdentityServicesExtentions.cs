using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServicesExtentions
    {

        public static IServiceCollection AddidentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                          {
                              options.TokenValidationParameters = new TokenValidationParameters
                              {
                                  ValidateIssuerSigningKey = true,

                                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),

                                  ValidateAudience = false,
                                  ValidateIssuer = false
                              };
                          });

        return services;

        }

    }
}