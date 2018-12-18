using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduNetcore.Data.Entities;

namespace TeduNetcore.Helper
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(AppUser appUser)
        {
            ClaimsPrincipal principal = await base.CreateAsync(appUser);
            System.Collections.Generic.IList<string> roles = await _userManager.GetRolesAsync(appUser);
            ClaimsIdentity claimsIdentity = principal.Identity as ClaimsIdentity;
            claimsIdentity.AddClaims(new[]
            {
                new Claim("Email",appUser.Email),
                new Claim("FullName",appUser.FullName),
                new Claim("Avatar",appUser.Avatar!=null?appUser.Avatar:string.Empty),
                new Claim("Roles",string.Join("-",roles))
            });
            return principal;
        }
    }
}
