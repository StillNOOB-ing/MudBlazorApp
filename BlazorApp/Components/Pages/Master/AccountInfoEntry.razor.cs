using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MudBlazorApp.Components.Pages.Master
{
    public class AccountInfoEntryBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] ISnackbar snackBar { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; }
        [Parameter] public MAccountInfo model { get; set; } = new MAccountInfo();
        [Parameter] public string mode { get; set; } = string.Empty;

        public List<MUserLevelRight> levelRightList { get; set; } = new List<MUserLevelRight>();

        public MUserLevelRight selectedLevelRight {  get; set; } = new MUserLevelRight();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            levelRightList = masterHelper.GetUserLevelRightDB().ToList();

            if (model != null)
            {
                if (levelRightList != null)
                {
                    selectedLevelRight = levelRightList.Where(x => x.UID == model.LevelRightID).FirstOrDefault();
                }  

                if (mode == "edit")
                {
                    model.ConfirmPassword = model.Password;
                }
                if (mode == "password")
                {
                    model.Password = string.Empty;
                }
            }
        }

        protected async Task OnValidSubmit(EditContext context)
        {
            if (mode == "add")
            {
                var result = await masterHelper.InsertAccountInfo(model);
                if (result.success)
                {
                    snackBar.Add("Add Successful", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "edit")
            {
                var result = await masterHelper.UpdateAccountInfo(model);
                if (result.success)
                {
                    snackBar.Add("Edit Successful", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "delete")
            {
                var result = await masterHelper.DeleteAccountInfo(model);
                if (result.success)
                {
                    snackBar.Add("Delete Successfully", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "password")
            {
                var result = await masterHelper.UpdateAccountInfo(model);
                if (result.success)
                {
                    snackBar.Add("Change Password Successfully", Severity.Success);
                    MudDialog.Close();
                }
            }
        }

        protected void OnCancel()
        {
            MudDialog.Close();
        }

        #region Event

        public void OnSelectedUserLevelRightChanged()
        {
            if (selectedLevelRight != null)
            {
                model.LevelRightID = selectedLevelRight.UID;
                model.LevelRightName = selectedLevelRight.Name;
            }
            else
            {
                model.LevelRightID = null;
                model.LevelRightName = null;
            }
        }

        #endregion
    }
}
