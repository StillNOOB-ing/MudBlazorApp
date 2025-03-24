using MudBlazorApp.Components.Database.Model;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MudBlazor;
using System.Linq.Expressions;

namespace MudBlazorApp.Components.CommonDataGrid
{
    public class StandardColumn
    {
        public string Title { get; set; }
        public LambdaExpression Expression { get; set; }
        public bool Hidden { get; set; }
        public SortDirection SortDirection { get; set; }

        public StandardColumn(string title, LambdaExpression expression, bool hidden = false, SortDirection sortDirection = SortDirection.Descending) 
        {
            Title = title;
            Expression = expression;
            Hidden = hidden;
            SortDirection = sortDirection;
        }
    }
}
