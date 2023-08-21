using System.ComponentModel;

namespace Task.DTOS
{
    public class UpdatedDto
    {

        public string? Title { get; set; }

        [DefaultValue("false")]
        public bool Completed { get; set; }
    }
}
