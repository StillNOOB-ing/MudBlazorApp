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
    public class TypeViewBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }
        public List<MType> dataList { get; set; }

        public List<StandardColumn> columns { get; set; } = new List<StandardColumn>()
        {
            new StandardColumn("UID", (Expression<Func<MType, int>>)(x => x.UID)),
            new StandardColumn("Name", (Expression<Func<MType, string?>>)(x => x.Name)),
            new StandardColumn("Description", (Expression<Func<MType, string?>>)(x => x.Description)),
            new StandardColumn("Created On", (Expression<Func<MType, DateTime?>>)(x => x.CreatedOn)),
            new StandardColumn("Created By", (Expression<Func<MType, string?>>)(x => x.CreatedBy)),
            new StandardColumn("Updated On", (Expression<Func<MType, DateTime?>>)(x => x.UpdatedOn)),
            new StandardColumn("Updated By", (Expression<Func<MType, string?>>)(x => x.UpdatedBy)),
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

            dataList = await masterHelper.GetTypeAll();
        }

        protected async Task ReloadGrid()
        {
            dataList = await masterHelper.GetTypeAll();

            StateHasChanged();
        }

        protected async Task OnActionButtonClicked(ActionInfo info)
        {
            if (info.ButtonType == ActionButtonType.Add)
            {
                var parameters = new DialogParameters 
                { 
                    { "model", new MType() },
                    { "mode", "add" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<TypeEntry>("Add", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }

        protected async Task OnCommandButtonClicked(CommandInfo<MType> info)
        {
            if (info.ButtonType == CommandButtonType.View)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "edit" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<TypeEntry>("View", parameters, options);
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
                var dialog = await dialogueService.ShowAsync<TypeEntry>("Edit", parameters, options);
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
                var dialog = await dialogueService.ShowAsync<TypeEntry>("Delete", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }
    }
}
