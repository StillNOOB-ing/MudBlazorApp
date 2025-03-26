using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace MudBlazorApp.Components.Pages.DailyTask
{
    public class DailyTaskViewBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] ITaskHelper taskHelper { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }
        public List<TDailyTask> dataList { get; set; }
        public int pageCount { get; set; }  
        public int pageIndex { get; set; }
        public int pageSize { get; set; } = 5;

        public List<StandardColumn> columns { get; set; } = new List<StandardColumn>()
        {
            new StandardColumn("UID", (Expression<Func<TDailyTask, int>>)(x => x.UID)),
            new StandardColumn("Report By ID", (Expression<Func<TDailyTask, string?>>)(x => x.ReportByID)),
            new StandardColumn("Reported On", (Expression<Func<TDailyTask, DateTime?>>)(x => x.ReportedOn)),
            new StandardColumn("Title", (Expression<Func<TDailyTask, string?>>)(x => x.Title)),
            new StandardColumn("Description", (Expression<Func<TDailyTask, string?>>)(x => x.Description)),
            new StandardColumn("Remark", (Expression<Func<TDailyTask, string?>>)(x => x.Remark)),
            new StandardColumn("PIC", (Expression<Func<TDailyTask, string?>>)(x => x.PICName)),
            new StandardColumn("Status", (Expression<Func<TDailyTask, string?>>)(x => x.StatusName)),
            new StandardColumn("Type", (Expression<Func<TDailyTask, string?>>)(x => x.TypeName)),
            new StandardColumn("Completed On", (Expression<Func<TDailyTask, DateTime?>>)(x => x.CompletedOn)),
            new StandardColumn("Created On", (Expression<Func<TDailyTask, DateTime?>>)(x => x.CreatedOn)),
            new StandardColumn("Created By", (Expression<Func<TDailyTask, string?>>)(x => x.CreatedBy)),
            new StandardColumn("Updated On", (Expression<Func<TDailyTask, DateTime?>>)(x => x.UpdatedOn)),
            new StandardColumn("Updated By", (Expression<Func<TDailyTask, string?>>)(x => x.UpdatedBy)),
            new StandardColumn("Color", (Expression<Func<TDailyTask, string?>>)(x => x.color)),
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

            var total = (await taskHelper.GetDailyTaskAll()).Count();
            pageCount = (total / pageSize) + (total % pageSize > 0 ? 1 : 0);

            dataList = await taskHelper.GetDailyTaskByPage(pageIndex, pageSize, Microsoft.Data.SqlClient.SortOrder.Ascending);
        }

        protected async Task ReloadGrid()
        {
            dataList = await taskHelper.GetDailyTaskByPage(pageIndex, pageSize, Microsoft.Data.SqlClient.SortOrder.Ascending);

            StateHasChanged();
        }

        protected async Task OnActionButtonClicked(ActionInfo info)
        {
            if (info.ButtonType == ActionButtonType.Add)
            {
                var parameters = new DialogParameters 
                { 
                    { "model", new TDailyTask() },
                    { "mode", "add" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<DailyTaskEntry>("Add", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }

        protected async Task OnCommandButtonClicked(CommandInfo<TDailyTask> info)
        {
            if (info.ButtonType == CommandButtonType.View)
            {
                var parameters = new DialogParameters
                {
                    { "model", info.Item },
                    { "mode", "edit" }
                };
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<DailyTaskEntry>("View", parameters, options);
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
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<DailyTaskEntry>("Edit", parameters, options);
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
                var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
                var dialog = await dialogueService.ShowAsync<DailyTaskEntry>("Delete", parameters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await ReloadGrid();
                }
            }
        }

        protected async Task OnPageChanged(int index)
        {
            pageIndex = index;

            await ReloadGrid();
        }
    }
}
