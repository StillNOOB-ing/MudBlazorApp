using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using MudBlazor.Extensions;
using MudBlazorApp.Components.CommonDataGrid;
using MudBlazorApp.Components.Database.Interface;
using MudBlazorApp.Components.Database.Model;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MudBlazorApp.Components.Pages.DailyTask
{
    public class DailyTaskEntryBase : ComponentBase
    {
        [Inject] IDialogService dialogueService { get; set; }
        [Inject] ISnackbar snackBar { get; set; }
        [Inject] ITaskHelper taskHelper { get; set; }
        [Inject] IMasterHelper masterHelper { get; set; }

        [CascadingParameter] private IMudDialogInstance MudDialog { get; set; }
        [Parameter] public TDailyTask model { get; set; }
        [Parameter] public string mode { get; set; }

        public List<MAccountInfo> picList { get; set; } = new List<MAccountInfo>();
        public List<MStatus> statusList { get; set; } = new List<MStatus>();
        public List<MType> typeList { get; set; } = new List<MType>();
        public MAccountInfo selectedPIC { get; set; }
        public MStatus selectedStatus { get; set; }
        public MType selectedType { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            picList = await masterHelper.GetAccountInfoAll();
            statusList = await masterHelper.GetStatusAll();
            typeList = await masterHelper.GetTypeAll();

            if (mode == "add")
            {
                model.ReportByID = "AUTO";
                model.ReportedOn = DateTime.Now;
            }
            else
            {
                selectedPIC = picList.Where(x => x.UID == model.PICID).FirstOrDefault();
                selectedStatus = statusList.Where(x => x.UID == model.StatusID).FirstOrDefault();
                selectedType = typeList.Where(x => x.UID == model.TypeID).FirstOrDefault();
            }

            await InvokeAsync(() => StateHasChanged());
        }

        protected async Task OnValidSubmit(EditContext context)
        {
            if (mode == "add")
            {
                var result = await taskHelper.InsertDailyTask(model);
                if (result.success)
                {
                    snackBar.Add("Add Successful", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "edit")
            {
                var result = await taskHelper.UpdateDailyTask(model);
                if (result.success)
                {
                    snackBar.Add("Edit Successful", Severity.Success);
                    MudDialog.Close();
                }
            }
            else if (mode == "delete")
            {
                var result = await taskHelper.DeleteDailyTask(model);
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

        public void OnSelectedPICChanged()
        {
            if (selectedStatus != null)
            {
                model.PICID = selectedPIC.UID;
                model.PICName = selectedPIC.Name;
            }
            else
            {
                model.PICID = null;
                model.PICName = null;
            }
        }

        public void OnSelectedStatusChanged()
        {
            if (selectedStatus != null)
            {
                model.StatusID = selectedStatus.UID;
                model.StatusName = selectedStatus.Name;

                if (selectedStatus.Name == "Completed")
                {
                    model.CompletedOn = DateTime.Now;
                }
                else
                {
                    model.CompletedOn = null;
                }
            }
            else
            {
                model.StatusID = null;
                model.StatusName = null;
            }
        }

        public void OnSelectedTypeChanged()
        {
            if (selectedType != null)
            {
                model.TypeID = selectedType.UID;
                model.TypeName = selectedType.Name;
            }
            else
            {
                model.TypeID = null;
                model.TypeName = null;
            }
        }

        #endregion
    }
}
