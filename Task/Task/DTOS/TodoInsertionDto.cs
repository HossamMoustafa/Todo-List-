using System.ComponentModel.DataAnnotations;

namespace Task.DTOS
{
    public class TodoInsertionDto
    {
       
        public string Title { get; set; }= String.Empty;
       
        public bool Completed { get; set; }= false;

        public string? UserId { get; set; } 

    }
}
