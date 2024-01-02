using System.ComponentModel.DataAnnotations.Schema;

namespace Fish_Farm.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public PositionType Position { get; set; }
        public string? ImageName { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public string? ImageSrc { get; set; }
        

    }
}
