using System.ComponentModel.DataAnnotations;

namespace NHLCafe.Pages
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, MinLength(2), MaxLength(128)]
        public string Name { get; set; }
    }
}
