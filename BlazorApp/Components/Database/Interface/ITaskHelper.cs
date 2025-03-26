using Microsoft.Data.SqlClient;
using MudBlazorApp.Components.Database.Model;

namespace MudBlazorApp.Components.Database.Interface
{
    public interface ITaskHelper
    {
        #region DailyTask

        public IQueryable<TDailyTask> GetDailyTaskDB();
        public Task<List<TDailyTask>> GetDailyTaskAll();
        public Task<ResultInfo> InsertDailyTask(TDailyTask item);
        public Task<ResultInfo> UpdateDailyTask(TDailyTask item);
        public Task<ResultInfo> DeleteDailyTask(TDailyTask item);
        public Task<List<TDailyTask>> GetDailyTaskRangeUID(int from, int to);
        public Task<List<TDailyTask>> GetDailyTaskRangeDate(DateTime from, DateTime to);
        public Task<List<TDailyTask>> GetDailyTaskByPage(int pageIndex, int pageSize, SortOrder sortOrder);


        #endregion
    }
}
