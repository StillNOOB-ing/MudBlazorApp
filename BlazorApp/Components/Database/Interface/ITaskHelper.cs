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

        #endregion
    }
}
