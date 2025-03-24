using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazorApp.Components.Database.Model;
using MudBlazorApp.Components.Database;

namespace MudBlazorApp.Components.Services
{
    public static class AccountService
    {

        private static IHttpContextAccessor HttpContextAccessor;

        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public static string GetUsername()
        {
            return HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
        }
    }
}
