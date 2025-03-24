using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
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
        [Parameter] public MAccountInfo model { get; set; }
        [Parameter] public string mode { get; set; }

        public List<MUserLevelRight> userLevelRightList { get; set; }
        public MUserLevelRight selectedUserLevelRight {  get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            userLevelRightList = new List<MUserLevelRight>();
            userLevelRightList = await masterHelper.GetUserLevelRightAll();
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
        }

        protected void OnCancel()
        {
            MudDialog.Close();
        }

        #region Event

        public void OnSelectedUserLevelRightChanged()
        {
            if (selectedUserLevelRight != null)
            {
                model.LevelRightID = selectedUserLevelRight.UID;
                model.LevelRightName = selectedUserLevelRight.Name;
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
