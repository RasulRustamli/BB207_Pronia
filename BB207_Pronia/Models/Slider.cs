using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BB207_Pronia.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Duzgun daxil edin")]
        public string Title { get; set; }
        [StringLength(15,ErrorMessage ="uzunluq max 10 ola biler")]
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get;set; }
    }
}
