namespace MudBlazorApp.Components.CommonDataGrid
{
    public enum CommandButtonType
    {
        Add, Edit, Delete, View
    }

    public class CommandInfo<T>
    {
        public T Item { get; set; }
        public CommandButtonType ButtonType { get; set; }

        public CommandInfo(T item, CommandButtonType buttonType)
        {
            Item = item;
            ButtonType = buttonType;
        }
    }
    public class StandardCommandButton
    {
        public CommandButtonType Type;

        private StandardCommandButton(CommandButtonType type) 
        {
            Type = type;
        }

        public static StandardCommandButton AddButton()
        {
            return new StandardCommandButton(CommandButtonType.Add);
        }
        public static StandardCommandButton EditButton()
        {
            return new StandardCommandButton(CommandButtonType.Edit);
        }
        public static StandardCommandButton DeleteButton()
        {
            return new StandardCommandButton(CommandButtonType.Delete);
        }
        public static StandardCommandButton ViewButton()
        {
            return new StandardCommandButton(CommandButtonType.View);
        }
    }
}

    
