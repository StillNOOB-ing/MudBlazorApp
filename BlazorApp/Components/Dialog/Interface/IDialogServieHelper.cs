using MudBlazor;

namespace MudBlazorApp.Components.Dialog.Interface
{
    public interface IDialogServieHelper
    {
        public Task<IDialogReference> AddEntryDialog<T>(T Item);
    }
}
