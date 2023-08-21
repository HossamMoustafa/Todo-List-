

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
    public class ApplicationUser:IdentityUser
    {

       
        public string? Address { get; set; }

        public   ICollection<Todo>? Todos { get; set; }    



    }
}
