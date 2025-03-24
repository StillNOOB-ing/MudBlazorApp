using MudBlazorApp.Components.Database.Model;

namespace MudBlazorApp.Components.Database.Interface
{
    public interface IMasterHelper
    {
        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB();
        public Task<List<MAccountInfo>> GetAccountInfoAll();
        public Task<ResultInfo> InsertAccountInfo(MAccountInfo item);
        public Task<ResultInfo> UpdateAccountInfo(MAccountInfo item);
        public Task<ResultInfo> DeleteAccountInfo(MAccountInfo item);

        #endregion

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB();
        public Task<List<MUserLevelRight>> GetUserLevelRightAll();
        public Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item);
        public Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item);
        public Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item);

        #endregion
    }
}
