using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static MudBlazor.CategoryTypes;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace MudBlazorApp.Components.Database.Helper
{
    public class TaskHelper: ITaskHelper
    {
        private readonly ITaskRepository repository;
        private readonly ProtectedSessionStorage sessionStorage;

        public TaskHelper(ITaskRepository Repository, ProtectedSessionStorage SessionStorage)
        {
            repository = Repository;
            sessionStorage = SessionStorage;
        }

        #region DailyTask

        public IQueryable<TDailyTask> GetDailyTaskDB()
        {
            return repository.GetDailyTaskDB(x => true);
        }
        public async Task<List<TDailyTask>> GetDailyTaskAll()
        {
            return await repository.GetDailyTaskAll(x => true);
        }
        public async Task<ResultInfo> InsertDailyTask(TDailyTask item)
        {
            var today = DateTime.Today;
            var tasklist = GetDailyTaskDB().Where(x => x.ReportedOn.Value.Date == DateTime.Today);

            int taskCount = tasklist.Count() + 1;
            string formattedDate = today.ToString("yyyyMMdd");
            string formattedCount = taskCount.ToString("D4");

            item.ReportByID = $"{formattedDate}{formattedCount}"; ;
            item.ReportedOn = DateTime.Now;

            item.CreatedOn = DateTime.Now;
            item.CreatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.InsertDailyTask(item);
        }
        public async Task<ResultInfo> UpdateDailyTask(TDailyTask item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.UpdateDailyTask(item);
        }
        public async Task<ResultInfo> DeleteDailyTask(TDailyTask item)
        {
            return await repository.DeleteDailyTask(item);
        }

        #endregion
    }
}
