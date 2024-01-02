namespace Fish_Farm.Entities
{
    public class ClientFishfarm
    {
        public int FishFarmId { get; set; }
        public ICollection<int> WorkersIdList { get; set; }
    }
}
