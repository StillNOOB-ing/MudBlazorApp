namespace MudBlazorApp.Components.CommonDataGrid
{
    public enum ActionButtonType
    {
        Add, Edit, Delete, View
    }

    public class ActionInfo
    {
        public ActionButtonType ButtonType { get; set; }

        public ActionInfo(ActionButtonType buttonType)
        {
            ButtonType = buttonType;
        }
    }


    public class StandardActionButton
    {
        public ActionButtonType Type;

        private StandardActionButton(ActionButtonType type) 
        {
            Type = type;
        }

        public static StandardActionButton AddButton()
        {
            return new StandardActionButton(ActionButtonType.Add);
        }
        public static StandardActionButton EditButton()
        {
            return new StandardActionButton(ActionButtonType.Edit);
        }
        public static StandardActionButton DeleteButton()
        {
            return new StandardActionButton(ActionButtonType.Delete);
        }
        public static StandardActionButton ViewButton()
        {
            return new StandardActionButton(ActionButtonType.View);
        }
    }
}

    
