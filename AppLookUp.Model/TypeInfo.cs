using System.ComponentModel.DataAnnotations;

namespace AppLookUp.Models
{
    public class TypeInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
