﻿using System.ComponentModel.DataAnnotations;

namespace MudBlazorApp.Components.Database.Model
{
    public class BaseDataColumn<T>
    {
        [Key] public int UID { get; set; }
        public DateTime? CreatedOn { get; set; }
        [MaxLength(50)] public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [MaxLength(50)] public string? UpdatedBy { get; set; }

        public T Clone()
        {
            return (T)this.MemberwiseClone();
        }
    }
}
