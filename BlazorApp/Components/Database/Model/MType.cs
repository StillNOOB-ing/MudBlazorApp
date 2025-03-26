using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MudBlazorApp.Components.Database.Model
{
    [Table("Type")]
    public partial class MType : BaseDataColumn<MType>
    {        
        [StringLength(50)] public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
