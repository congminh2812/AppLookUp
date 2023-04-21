using System.ComponentModel.DataAnnotations;

namespace AppLookUp.Models
{
    public class Component
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
    }
}
