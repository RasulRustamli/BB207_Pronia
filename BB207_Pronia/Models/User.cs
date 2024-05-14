using Microsoft.AspNetCore.Identity;

namespace BB207_Pronia.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
