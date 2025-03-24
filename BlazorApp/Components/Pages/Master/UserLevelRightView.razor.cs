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
    public class UserLevelRightViewBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }
        public List<MUserLevelRight> dataList { get; set; }

        public List<StandardColumn> columns { get; set; } = new List<StandardColumn>()
        {
            new StandardColumn("UID", (Expression<Func<MUserLevelRight, int>>)(x => x.UID)),
            new StandardColumn("Name", (Expression<Func<MUserLevelRight, string?>>)(x => x.Name)),
            new StandardColumn("Description", (Expression<Func<MUserLevelRight, string?>>)(x => x.Description)),
            new StandardColumn("CreatedOn", (Expression<Func<MUserLevelRight, DateTime?>>)(x => x.CreatedOn)),
            new StandardColumn("CreatedBy", (Expression<Func<MUserLevelRight, string?>>)(x => x.CreatedBy)),
            new StandardColumn("UpdatedOn", (Expression<Func<MUserLevelRight, DateTime?>>)(x => x.UpdatedOn)),
            new StandardColumn("CreatedBy", (Expression<Func<MUserLevelRight, string?>>)(x => x.UpdatedBy)),
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

            dataList = await masterHelper.GetUserLevelRightAll();
        }

        protected async Task ReloadGrid()
        {
            dataList = await masterHelper.GetUserLevelRightAll();

            StateHasChanged();
        }

        protected async Task OnActionButtonClicked(ActionInfo info)
        {
            if (info.ButtonType == ActionButtonType.Add)
            {
                var parameters = new DialogParameters 
                { 
                    { "model", new MUserLevelRight() },
                    { "mode", "add" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<UserLevelRightEntry>("Add", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }

        protected async Task OnCommandButtonClicked(CommandInfo<MUserLevelRight> info)
        {
            if (info.ButtonType == CommandButtonType.View)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "edit" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<UserLevelRightEntry>("View", parameters, options);
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
                var dialog = await dialogueService.ShowAsync<UserLevelRightEntry>("Edit", parameters, options);
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
                var dialog = await dialogueService.ShowAsync<UserLevelRightEntry>("Delete", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }
    }
}
