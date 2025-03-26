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
using Microsoft.Data.SqlClient;


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
        public async Task<List<TDailyTask>> GetDailyTaskRangeUID(int from,  int to)
        {
            return await repository.GetDailyTaskAll(x => x.UID >= from && x.UID <= to);
        }
        public async Task<List<TDailyTask>> GetDailyTaskRangeDate(DateTime from, DateTime to)
        {
            return await repository.GetDailyTaskAll(x => x.ReportedOn >= from && x.ReportedOn <= to);
        }
        public async Task<List<TDailyTask>> GetDailyTaskByPage(int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Descending)
        {
            List<TDailyTask> list = await repository.GetDailyTaskAll(x => true);            

            if (sortOrder == SortOrder.Ascending)
            {
                list = list.OrderBy(x => x.UID).ToList();
            }
            else if (sortOrder == SortOrder.Descending)
            {
                list = list.OrderByDescending(x => x.UID).ToList();
            }

            int skip = (pageIndex - 1) * pageSize;
            int take = pageSize;
            list = list.Skip(skip).Take(take).ToList();

            return list;
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
