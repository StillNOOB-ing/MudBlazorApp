using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace MudBlazorApp.Components.Pages.Master
{
    public class StatusViewBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }
        public List<MStatus> dataList { get; set; }

        public List<StandardColumn> columns { get; set; } = new List<StandardColumn>()
        {
            new StandardColumn("UID", (Expression<Func<MStatus, int>>)(x => x.UID)),
            new StandardColumn("Name", (Expression<Func<MStatus, string?>>)(x => x.Name)),
            new StandardColumn("Description", (Expression<Func<MStatus, string?>>)(x => x.Description)),
            new StandardColumn("Created On", (Expression<Func<MStatus, DateTime?>>)(x => x.CreatedOn)),
            new StandardColumn("Created By", (Expression<Func<MStatus, string?>>)(x => x.CreatedBy)),
            new StandardColumn("Updated On", (Expression<Func<MStatus, DateTime?>>)(x => x.UpdatedOn)),
            new StandardColumn("Updated By", (Expression<Func<MStatus, string?>>)(x => x.UpdatedBy)),
        };

        public List<StandardActionButton> actionButtons { get; set; } = new List<StandardActionButton>()
        {
            StandardActionButton.AddButton()
        };

        public List<StandardCommandButton> commandButtons { get; set; } = new List<StandardCommandButton>()
        {
            StandardCommandButton.ViewButton(),
            StandardCommandButton.EditButton(),
            StandardCommandButton.DeleteButton(),
        };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            dataList = await masterHelper.GetStatusAll();
        }

        protected async Task ReloadGrid()
        {
            dataList = await masterHelper.GetStatusAll();

            StateHasChanged();
        }

        protected async Task OnActionButtonClicked(ActionInfo info)
        {
            if (info.ButtonType == ActionButtonType.Add)
            {
                var parameters = new DialogParameters 
                { 
                    { "model", new MStatus() },
                    { "mode", "add" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<StatusEntry>("Add", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }

        protected async Task OnCommandButtonClicked(CommandInfo<MStatus> info)
        {
            if (info.ButtonType == CommandButtonType.View)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "edit" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<StatusEntry>("View", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
            else if (info.ButtonType == CommandButtonType.Edit)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "edit" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<StatusEntry>("Edit", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
            else if (info.ButtonType == CommandButtonType.Delete)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "delete" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<StatusEntry>("Delete", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }
    }
}
