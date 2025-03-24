namespace MudBlazorApp.Components.Database
{
    public class ResultInfo
    {
        public bool success { get; set; } = false;
        public string message { get; set; } = string.Empty;

        public ResultInfo(bool isSuccessfull, string msg = null) 
        {
            success = isSuccessfull;
            message = msg;
        }
    }
}
