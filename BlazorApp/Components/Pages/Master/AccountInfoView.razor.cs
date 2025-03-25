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
    public class AccountInfoViewBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }
        public List<MAccountInfo> dataList { get; set; }

        public List<StandardColumn> columns { get; set; } = new List<StandardColumn>()
        {
            new StandardColumn("UID", (Expression<Func<MAccountInfo, int>>)(x => x.UID)),
            new StandardColumn("Name", (Expression<Func<MAccountInfo, string?>>)(x => x.Name)),
            new StandardColumn("Active", (Expression<Func<MAccountInfo, bool>>)(x => x.Active)),
            new StandardColumn("Level Right", (Expression<Func<MAccountInfo, string?>>)(x => x.LevelRightName)),
            new StandardColumn("Created On", (Expression<Func<MAccountInfo, DateTime?>>)(x => x.CreatedOn)),
            new StandardColumn("Created By", (Expression<Func<MAccountInfo, string?>>)(x => x.CreatedBy)),
            new StandardColumn("Updated On", (Expression<Func<MAccountInfo, DateTime?>>)(x => x.UpdatedOn)),
            new StandardColumn("Updated By", (Expression<Func<MAccountInfo, string?>>)(x => x.UpdatedBy)),
        };

        public List<StandardActionButton> actionButtons { get; set; } = new List<StandardActionButton>()
        {
            StandardActionButton.AddButton()
        };

        public List<StandardCommandButton> commandButtons { get; set; } = new List<StandardCommandButton>()
        {
            StandardCommandButton.EditButton(),
            StandardCommandButton.DeleteButton(),
            StandardCommandButton.ChangePasswordButton(),
        };

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            dataList = await masterHelper.GetAccountInfoAll();
        }

        protected async Task ReloadGrid()
        {
            dataList = await masterHelper.GetAccountInfoAll();

            StateHasChanged();
        }

        protected async Task OnActionButtonClicked(ActionInfo info)
        {
            if (info.ButtonType == ActionButtonType.Add)
            {
                var parameters = new DialogParameters 
                { 
                    { "model", new MAccountInfo() },
                    { "mode", "add" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<AccountInfoEntry>("Add", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }

        protected async Task OnCommandButtonClicked(CommandInfo<MAccountInfo> info)
        {
            if (info.ButtonType == CommandButtonType.Edit)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "edit" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<AccountInfoEntry>("Edit", parameters, options);
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
                var dialog = await dialogueService.ShowAsync<AccountInfoEntry>("Delete", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
            else if (info.ButtonType == CommandButtonType.ChangePassword)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "password" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<AccountInfoEntry>("Change Password", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }
    }
}
