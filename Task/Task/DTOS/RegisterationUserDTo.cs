using System.ComponentModel.DataAnnotations;

namespace Task.DTOS
{
    public class RegisterationUserDTo
    {
        [Required]
        public string UserName { get; set; }=String.Empty; 

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        public string Email { get; set; }=String.Empty;


        public string Address { get; set; }= String.Empty;


    }
}
