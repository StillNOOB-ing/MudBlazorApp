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
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MudBlazorApp.Components.Pages
{    
    public class LoginBase: ComponentBase
    {
        [Inject] IMasterHelper masterHelper { get; set; }
        [Inject] IPasswordHasher<MAccountInfo> passwordHasher { get; set; }
        [Inject] ISnackbar snackBar { get; set; }
        [Inject] ProtectedSessionStorage sessionStorage { get; set; }

        [Inject] NavigationManager navigationManager { get;set; }

        public LoginModel loginModel { get; set; }
        public string errorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            loginModel = new LoginModel();

            var sessionUser = await sessionStorage.GetAsync<string>("username");

            if (sessionUser.Success && !string.IsNullOrEmpty(sessionUser.Value))
            {
                navigationManager.NavigateTo("/", forceLoad: true);
            }
        }

        public async Task OnValidSubmit(EditContext context)
        {
            var accountList = await masterHelper.GetAccountInfoAll();

            var user = accountList.Where(x => x.Email == loginModel.Email).FirstOrDefault();

            if (user == null)
            {
                errorMessage = "User not found.";
                return;
            }
            if (!user.Active)
            {
                errorMessage = "User has been deactivated.";
                return;
            }

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, loginModel.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                errorMessage = "Invalid email or password.";
                return;
            }

            snackBar.Add("Login successful!", Severity.Success);

            //user.LastLoginOn = DateTime.Now;
            //var result = await masterHelper.UpdateAccountInfo(user);

            await sessionStorage.SetAsync("username", user.Name);

            navigationManager.NavigateTo("/", forceLoad: true);
        }

        public class LoginModel
        {
            [Required, EmailAddress] public string Email { get; set; } = string.Empty;
            [Required] public string Password { get; set; } = string.Empty;
            public bool RememberMe { get; set; }
        }
    }
}
