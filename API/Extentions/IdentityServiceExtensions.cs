using Standard.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration config)
        {
            var builder = services.AddIdentityCore<AppUser>(options =>
            {
                // Configure identity options if needed
            });

            //builder = new IdentityBuilder(builder.UserType, typeof(AppRole), builder.Services);
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            //builder.AddRoleManager<RoleManager<AppRole>>(); // Add RoleManager for AppRole

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidateIssuer = false,
                        ValidIssuer = config["Token:Issuer"],
                        ValidateAudience = false,
                    };
                });

            return services;
        }

        //public static async Task AddRoles(RoleManager<AppRole> roleManager)
        //{
        //    var roles = new List<string> { "Admin", "Customer", "Seller","Super Admin","Senior", "User" };

        //    foreach (var role in roles)
        //    {
        //        if (!await roleManager.RoleExistsAsync(role))
        //        {
        //            await roleManager.CreateAsync(new AppRole(role));
        //        }
        //    }
        //}
    }
}