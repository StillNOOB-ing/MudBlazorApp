using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;

namespace MudBlazorApp.Components.Database.Interface
{
    public interface ITaskRepository
    {
        #region DailyTask

        public IQueryable<TDailyTask> GetDailyTaskDB(Expression<Func<TDailyTask, bool>> predicate);
        public Task<List<TDailyTask>> GetDailyTaskAll(Expression<Func<TDailyTask, bool>> predicate);
        public Task<ResultInfo> InsertDailyTask(TDailyTask item);
        public Task<ResultInfo> UpdateDailyTask(TDailyTask item);
        public Task<ResultInfo> DeleteDailyTask(TDailyTask item);

        #endregion
    }
}
