using Fish_Farm.Entities;

namespace Fish_Farm.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FishFarm>? fishFarms { get; set; }
        public ICollection<Worker>? Workers { get; set; }
    }
}
