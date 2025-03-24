using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MudBlazorApp.Components.Pages.Master
{
    public class UserLevelRightEntryBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] ISnackbar snackBar { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; }
        [Parameter] public MUserLevelRight model { get; set; }
        [Parameter] public string mode { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected async Task OnValidSubmit(EditContext context)
        {
            if (mode == "add")
            {
                var result = await masterHelper.InsertUserLevelRight(model);
                if (result.success)
                {
                    snackBar.Add("Add Successful", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "edit")
            {
                var result = await masterHelper.UpdateUserLevelRight(model);
                if (result.success)
                {
                    snackBar.Add("Edit Successful", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "delete")
            {
                var result = await masterHelper.DeleteUserLevelRight(model);
                if (result.success)
                {
                    snackBar.Add("Delete Successfully", Severity.Success);
                    MudDialog.Close();
                }
            }
        }

        protected void OnCancel()
        {
            MudDialog.Close();
        }
    }
}
