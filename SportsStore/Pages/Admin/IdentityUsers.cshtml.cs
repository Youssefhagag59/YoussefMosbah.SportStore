using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace SportsStore.Pages.Admin
{
    [Authorize]
    public class IdentityUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        
        public IdentityUsersModel (UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IdentityUser AdminUser { get; set; } = new();
        public async Task OnGetAsync()
        {
            AdminUser = await _userManager.FindByNameAsync("Admin");
        }
    }
}
