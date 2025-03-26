using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace MudBlazorApp.Components.CommonDataGrid
{
    

    public class CommonDataGridBase<T> : ComponentBase
    {
        [Parameter] public string GridTitle { get; set; } = string.Empty;
        [Parameter] public List<T> Data {  get; set; } = new List<T>();
        [Parameter] public List<StandardColumn> Columns { get; set; } = new List<StandardColumn>();
        [Parameter] public List<StandardActionButton> ActionButtons { get; set; } = new List<StandardActionButton>();
        [Parameter] public List<StandardCommandButton> CommandButtons { get; set; } = new List<StandardCommandButton>();
        [Parameter] public EventCallback<ActionInfo> OnActionButtonClick {  get; set; }
        [Parameter] public EventCallback<CommandInfo<T>> OnCommandButtonClick { get; set; }
        [Parameter] public EventCallback<int> OnPageChanged { get; set; }
        [Parameter] public int PageCount { get; set; }
        [Parameter] public int PageSize { get; set; } = 10;
        [Parameter] public bool ReadOnly { get; set; } = false;
        [Parameter] public bool Hover { get; set; } = true;
        [Parameter] public bool Stripped { get; set; } = false;
        [Parameter] public bool Bordered { get; set; } = true;
        [Parameter] public bool Dense { get; set; } = true;

        public MudDataGrid<T>? Grid { get; set; }
        public string SearchText { get; set; } = string.Empty;
        
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //if (Grid != null)
            //{
            //    foreach (var column in Columns)
            //    {
            //        if (column.Expression is LambdaExpression lambda)
            //        {
            //            var compiledFunc = lambda.Compile();
            //            await Grid.SetSortAsync(column.Title, column.SortDirection, item => compiledFunc.DynamicInvoke(item));
            //        }
            //    }
            //}

            await InvokeAsync(() => StateHasChanged());
        }

        public RenderFragment RenderColumns() => builder =>
        {
            foreach (var column in Columns)
            {
                builder.OpenComponent(0, typeof(MudBlazor.PropertyColumn<,>).MakeGenericType(typeof(T), column.Expression.ReturnType));
                builder.AddAttribute(1, "Title", column.Title);
                builder.AddAttribute(2, "Property", column.Expression);
                builder.AddAttribute(3, "Hidden", column.Hidden);
                builder.AddAttribute(4, "Sortable", true);
                builder.CloseComponent();                
            }
        };

        public Func<T, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                return true;
            }

            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(x)?.ToString();
                if (!string.IsNullOrEmpty(value) && value.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        };
    }
}
