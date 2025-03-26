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


namespace MudBlazorApp.Components.Database.Helper
{
    public class MasterHelper: IMasterHelper
    {      
        private readonly IMasterRepository repository;
        private readonly IPasswordHasher<MAccountInfo> passwordHasher;
        private readonly ProtectedSessionStorage sessionStorage;

        public MasterHelper(IMasterRepository masterRepository, IPasswordHasher<MAccountInfo> PasswordHasher, ProtectedSessionStorage SessionStorage)
        {
            repository = masterRepository;
            passwordHasher = PasswordHasher;
            sessionStorage = SessionStorage;
        }

        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB()
        {
            return repository.GetAccountInfoDB(x => true);
        }
        public async Task<List<MAccountInfo>> GetAccountInfoAll()
        {
            return await repository.GetAccountInfoAll(x => true);
        }
        public async Task<ResultInfo> InsertAccountInfo(MAccountInfo item)
        {
            item.Password = passwordHasher.HashPassword(item, item.Password);

            item.CreatedOn = DateTime.Now;
            item.CreatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.InsertAccountInfo(item);
        }
        public async Task<ResultInfo> UpdateAccountInfo(MAccountInfo item)
        {
            item.Password = passwordHasher.HashPassword(item, item.Password);

            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.UpdateAccountInfo(item);
        }
        public async Task<ResultInfo> DeleteAccountInfo(MAccountInfo item)
        {
            return await repository.DeleteAccountInfo(item);
        }

        #endregion

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB()
        {
            return repository.GetUserLevelRightDB(x => true);
        }
        public async Task<List<MUserLevelRight>> GetUserLevelRightAll()
        {
            return await repository.GetUserLevelRightAll(x => true);
        }
        public async Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item)
        {           
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.InsertUserLevelRight(item);
        }
        public async Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.UpdateUserLevelRight(item);
        }
        public async Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item)
        {
            return await repository.DeleteUserLevelRight(item);
        }

        #endregion

        #region Status

        public IQueryable<MStatus> GetStatusDB()
        {
            return repository.GetStatusDB(x => true);
        }
        public async Task<List<MStatus>> GetStatusAll()
        {
            return await repository.GetStatusAll(x => true);
        }
        public async Task<ResultInfo> InsertStatus(MStatus item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.InsertStatus(item);
        }
        public async Task<ResultInfo> UpdateStatus(MStatus item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.UpdateStatus(item);
        }
        public async Task<ResultInfo> DeleteStatus(MStatus item)
        {
            return await repository.DeleteStatus(item);
        }

        #endregion

        #region Type

        public IQueryable<MType> GetTypeDB()
        {
            return repository.GetTypeDB(x => true);
        }
        public async Task<List<MType>> GetTypeAll()
        {
            return await repository.GetTypeAll(x => true);
        }
        public async Task<ResultInfo> InsertType(MType item)
        {
            item.CreatedOn = DateTime.Now;
            item.CreatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.InsertType(item);
        }
        public async Task<ResultInfo> UpdateType(MType item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = (await sessionStorage.GetAsync<string>("username")).Value;

            return await repository.UpdateType(item);
        }
        public async Task<ResultInfo> DeleteType(MType item)
        {
            return await repository.DeleteType(item);
        }

        #endregion
    }
}
