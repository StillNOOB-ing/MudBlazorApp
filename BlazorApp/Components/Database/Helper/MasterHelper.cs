using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MudBlazorApp.Components.Services;


namespace MudBlazorApp.Components.Database.Helper
{
    public class MasterHelper: IMasterHelper
    {
        private readonly ApplicationDbContext DbContext;
        private readonly IPasswordHasher<MAccountInfo> PasswordHasher;
        
        public MasterHelper(ApplicationDbContext db, IPasswordHasher<MAccountInfo> passwordHasher)
        {
            DbContext = db;
            PasswordHasher = passwordHasher;
        }

        #region AccountInfo

        public IQueryable<MAccountInfo> GetAccountInfoDB()
        {
            return DbContext.AccountInfos.AsNoTracking().AsQueryable();
        }
        public async Task<List<MAccountInfo>> GetAccountInfoAll()
        {
            return await DbContext.AccountInfos.AsNoTracking().ToListAsync();
        }
        public async Task<ResultInfo> InsertAccountInfo(MAccountInfo item)
        {
            try
            {
                var findItem = new MAccountInfo();
                findItem = item.Clone();

                findItem.Password = PasswordHasher.HashPassword(findItem, item.Password);

                findItem.CreatedOn = DateTime.Now;
                findItem.CreatedBy = AccountService.GetUsername();

                await DbContext.AccountInfos.AddAsync(findItem);
                await DbContext.SaveChangesAsync();

                return new ResultInfo(true);
            }
            catch (Exception ex)
            {
                return new ResultInfo(false, ex.Message);
            }
        }
        public async Task<ResultInfo> UpdateAccountInfo(MAccountInfo item)
        {
            try
            {
                var findItem = new MAccountInfo();
                findItem = GetAccountInfoDB().Where(x => x.UID == item.UID).FirstOrDefault();
                if (findItem != null)
                {
                    findItem = item.Clone();

                    findItem.Password = PasswordHasher.HashPassword(findItem, item.Password);

                    item.UpdatedOn = DateTime.Now;
                    item.UpdatedBy = AccountService.GetUsername();

                    DbContext.AccountInfos.Update(findItem);
                    await DbContext.SaveChangesAsync();

                    return new ResultInfo(true);
                }
                else
                {
                    return new ResultInfo(false, "No item found!");
                }
            }
            catch (Exception ex)
            {
                return new ResultInfo(false, ex.Message);
            }
        }
        public async Task<ResultInfo> DeleteAccountInfo(MAccountInfo item)
        {
            try
            {
                var findItem = new MAccountInfo();
                findItem = GetAccountInfoDB().Where(x => x.UID == item.UID).FirstOrDefault();
                if (findItem != null)
                {
                    DbContext.AccountInfos.Remove(findItem);
                    await DbContext.SaveChangesAsync();

                    return new ResultInfo(true);
                }
                else
                {
                    return new ResultInfo(false, "No item found!");
                }
            }
            catch (Exception ex)
            {
                return new ResultInfo(false, ex.Message);
            }
        }

        #endregion

        #region UserLevelRight

        public IQueryable<MUserLevelRight> GetUserLevelRightDB()
        {
            return DbContext.UserLevelRights.AsNoTracking().AsQueryable();
        }
        public async Task<List<MUserLevelRight>> GetUserLevelRightAll()
        {
            return await DbContext.UserLevelRights.AsNoTracking().ToListAsync();
        }
        public async Task<ResultInfo> InsertUserLevelRight(MUserLevelRight item)
        {
            try
            {
                var findItem = new MUserLevelRight();
                findItem = item.Clone();

                findItem.CreatedOn = DateTime.Now;
                findItem.CreatedBy = AccountService.GetUsername();

                await DbContext.UserLevelRights.AddAsync(findItem);
                await DbContext.SaveChangesAsync();

                return new ResultInfo(true);
            }
            catch (Exception ex)
            {
                return new ResultInfo(false, ex.Message);
            }
        }
        public async Task<ResultInfo> UpdateUserLevelRight(MUserLevelRight item)
        {
            try
            {
                var findItem = new MUserLevelRight();
                findItem = GetUserLevelRightDB().Where(x => x.UID == item.UID).FirstOrDefault();
                if (findItem != null)
                {
                    findItem = item.Clone();

                    item.UpdatedOn = DateTime.Now;
                    item.UpdatedBy = AccountService.GetUsername();

                    DbContext.UserLevelRights.Update(findItem);
                    await DbContext.SaveChangesAsync();

                    return new ResultInfo(true);
                }
                else
                {
                    return new ResultInfo(false, "No item found!");
                }                    
            }
            catch (Exception ex)
            {
                return new ResultInfo(false, ex.Message);
            }
        }
        public async Task<ResultInfo> DeleteUserLevelRight(MUserLevelRight item)
        {
            try
            {
                var findItem = new MUserLevelRight();
                findItem = GetUserLevelRightDB().Where(x => x.UID == item.UID).FirstOrDefault();
                if (findItem != null)
                {
                    DbContext.UserLevelRights.Remove(findItem);
                    await DbContext.SaveChangesAsync();

                    return new ResultInfo(true);
                }
                else
                {
                    return new ResultInfo(false, "No item found!");
                }
            }
            catch (Exception ex)
            {
                return new ResultInfo(false, ex.Message);
            }
        }

        #endregion
    }
}
