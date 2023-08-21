

using System.ComponentModel;

namespace Task.DTOS
{
    public class GetTods
    {
        public string? UserId { get; set; }

        public  int?  id  { get; set; }

        public string? Title { get; set; }

        [DefaultValue("false")]
        public bool Completed { get; set; }

      





    }
}
