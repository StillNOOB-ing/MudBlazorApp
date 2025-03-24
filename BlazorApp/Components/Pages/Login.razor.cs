using Microsoft.AspNetCore.Components;
using MudBlazorApp.Components.Database;
using Microsoft.EntityFrameworkCore;
using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazor;
using MudBlazorApp.Components.Database.Interface;

namespace MudBlazorApp.Components.Pages
{    
    public class LoginBase: ComponentBase
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

        } 
    }
}
