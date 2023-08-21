using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Task.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }




      //  [DefaultValue(false)]
        [DefaultValue(false)]
        public bool Completed { get; set; }


      //  [ForeignKey("ApplicationUser")]
        public string? ApplicationUserId { get; set; }  


        public  ApplicationUser? ApplicationUser { get; set; }




    }
}
