using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Marek.Prihlasenie.Pages;

public class LoginModel : PageModel
{
    public async Task OnGet(string redirectUri)
    {
        await HttpContext.ChallengeAsync("OpenIdConnect", new 
            AuthenticationProperties { RedirectUri = redirectUri } );
    }  
}