using AppLookUp.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppLookUp.Models
{
    public class Information : IAuditable, IDeletable
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Loại")]
        public int TypeInfoId { get; set; }

        [ForeignKey("TypeInfoId")]
        [ValidateNever]
        public TypeInfo TypeInfo { get; set; }

        [ValidateNever]
        public DateTime CreatedTime { get; set; }

        [ValidateNever]
        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        [ValidateNever]
        public AppUser CreatedUser { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string? UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        [ValidateNever]
        public AppUser UpdatedUser { get; set; }

        public bool IsDeleted { get; set; }
    }
}
