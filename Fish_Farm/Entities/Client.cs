namespace Fish_Farm.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string? ClientEmail { get; set; }
        public ICollection<FishFarm>? fishFarms { get; set; }
        public ICollection<Worker>? Workers { get; set; }
    }
}
