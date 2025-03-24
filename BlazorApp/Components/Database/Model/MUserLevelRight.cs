using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MudBlazorApp.Components.Database.Model
{
    [Table("UserLevelRight")]
    public partial class MUserLevelRight : BaseDataColumn<MUserLevelRight>
    {        
        [StringLength(50)] 
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
