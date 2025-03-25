using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static MudBlazor.CategoryTypes;


namespace MudBlazorApp.Components.Database.Helper
{
    public class MasterHelper: IMasterHelper
    {      
        private readonly IMasterRepository repository;
        private readonly IPasswordHasher<MAccountInfo> PasswordHasher;

        public MasterHelper(IMasterRepository masterRepository, IPasswordHasher<MAccountInfo> passwordHasher)
        {
            repository = masterRepository;
            PasswordHasher = passwordHasher;
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
            item.Password = PasswordHasher.HashPassword(item, item.Password);

            item.CreatedOn = DateTime.Now;
            item.CreatedBy = "";

            return await repository.InsertAccountInfo(item);
        }
        public async Task<ResultInfo> UpdateAccountInfo(MAccountInfo item)
        {
            item.Password = PasswordHasher.HashPassword(item, item.Password);

            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = "";

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
            item.CreatedBy = "";

            return await repository.InsertUserLevelRight(item);
        }
        public async Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item)
        {
            item.UpdatedOn = DateTime.Now;
            item.UpdatedBy = "";

            return await repository.UpdateUserLevelRight(item);
        }
        public async Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item)
        {
            return await repository.DeleteUserLevelRight(item);
        }

        #endregion
    }
}
