using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MudBlazorApp.Components.Database.Model
{
    [Table("Status")]
    public partial class MStatus : BaseDataColumn<MStatus>
    {        
        [StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
