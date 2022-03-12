using ExampleIdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExampleIdentityApp.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public IndexModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _usermanager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public RegisterModel registerModel { get; set; }
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostRegister()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registerModel.Email, Email = registerModel.Email };
                var result = await _usermanager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var username = User.Identity.Name;
                }
            }
            return RedirectToPage("/Index");
        }
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
