using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Standard.Entities.Identity;
using System.Security.Claims;

namespace API.Extentions
{
    public static class UserMangerExtension
    {
        public static async Task<AppUser> FindByUserByClaimsPrinciplelWithAddressAsync(this UserManager<AppUser>
            input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email ==
            email);
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrinciple(this UserManager<AppUser>
            input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email ==
            email);
        }


        public static async Task<AppUser> FindAddresByEmailAsync(this UserManager<AppUser> input, string email)
        {
            return await input.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
        }



    }
}
