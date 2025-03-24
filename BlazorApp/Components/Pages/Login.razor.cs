using Microsoft.AspNetCore.Components;
using MudBlazorApp.Components.Database;
using Microsoft.EntityFrameworkCore;
using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazor;
using MudBlazorApp.Components.Database.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace MudBlazorApp.Components.Pages
{    
    public class LoginBase: ComponentBase
    {
        [Inject] SignInManager<IdentityUser> SignInManager { get; set; }
        [Inject] NavigationManager navigationManager { get;set; }
        [Inject] ISnackbar snackBar { get; set; }

        public LoginModel loginModel { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            loginModel = new LoginModel();
        }

        public async Task OnValidSubmit(EditContext context)
        {
            var result = await SignInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                snackBar.Add("Login successful!", Severity.Success);
                navigationManager.NavigateTo("/", forceLoad: true);
            }
            else
            {
                errorMessage = "Invalid email or password.";
            }
        }

        public class LoginModel
        {
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            [Required] public string Password { get; set; } = string.Empty;
            public bool RememberMe { get; set; }
        }
    }
}
