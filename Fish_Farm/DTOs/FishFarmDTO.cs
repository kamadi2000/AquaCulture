using System.ComponentModel.DataAnnotations.Schema;

namespace Fish_Farm.DTOs
{
    public class FishFarmDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClientId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Num_of_cages { get; set; }
        public string? ImageName { get; set; }
        public Boolean Has_barge { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        public string? ImageSrc { get; set; }
    }
}
