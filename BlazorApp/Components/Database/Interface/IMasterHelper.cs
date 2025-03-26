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

        #region Status

        public IQueryable<MStatus> GetStatusDB();
        public Task<List<MStatus>> GetStatusAll();
        public Task<ResultInfo> InsertStatus(MStatus item);
        public Task<ResultInfo> UpdateStatus(MStatus item);
        public Task<ResultInfo> DeleteStatus(MStatus item);

        #endregion

        #region Type

        public IQueryable<MType> GetTypeDB();
        public Task<List<MType>> GetTypeAll();
        public Task<ResultInfo> InsertType(MType item);
        public Task<ResultInfo> UpdateType(MType item);
        public Task<ResultInfo> DeleteType(MType item);

        #endregion
    }
}
