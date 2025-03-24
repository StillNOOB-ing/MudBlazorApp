using MudBlazorApp.Components.Database.Model;
using MudBlazorApp.Components.Dialog.Interface;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MudBlazorApp.Components.Dialog.Helper
{
    public class DialogServiceHelper : IDialogServieHelper
    {        
        private readonly IDialogService _dialogService;

        public DialogServiceHelper(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task<IDialogReference> AddEntryDialog<T>(T Item)
        {
            var option = new DialogOptions { };
            //var parameter = new DialogParameters { ["Item"] = Item };
            return await _dialogService.ShowAsync<EntryDialog<T>>("Form", option);
        }
    }
}
