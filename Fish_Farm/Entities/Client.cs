namespace Fish_Farm.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FishFarm>? fishFarms { get; set; }
    }
}
