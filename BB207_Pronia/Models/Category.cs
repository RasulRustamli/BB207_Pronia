using System.ComponentModel.DataAnnotations;

namespace BB207_Pronia.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Duzgun doldur!!!!!!!")]
        public string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
