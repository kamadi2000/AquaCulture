using Fish_Farm.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fish_Farm.DTOs
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public PositionType Position { get; set; }
        public int? FishFarmId { get; set; }
        public string? ImageName { get; set; }
        public int? ClientId { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public string? ImageSrc { get; set; }
    }
}
