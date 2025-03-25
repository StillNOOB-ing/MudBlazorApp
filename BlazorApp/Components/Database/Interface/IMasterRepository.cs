using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;

namespace MudBlazorApp.Components.Database.Interface
{
    public interface IMasterRepository
    {
        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB(Expression<Func<MAccountInfo, bool>> predicate);
        public Task<List<MAccountInfo>> GetAccountInfoAll(Expression<Func<MAccountInfo, bool>> predicate);
        public Task<ResultInfo> InsertAccountInfo(MAccountInfo item);
        public Task<ResultInfo> UpdateAccountInfo(MAccountInfo item);
        public Task<ResultInfo> DeleteAccountInfo(MAccountInfo item);

        #endregion

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB(Expression<Func<MUserLevelRight, bool>> predicate);
        public Task<List<MUserLevelRight>> GetUserLevelRightAll(Expression<Func<MUserLevelRight, bool>> predicate);
        public Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item);
        public Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item);
        public Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item);

        #endregion
    }
}
