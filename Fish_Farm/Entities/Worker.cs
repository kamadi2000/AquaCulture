namespace Fish_Farm.Entities
{
    public class Worker
    {
        public int Id { get; set; }

        public PositionType Position { get; set; }
        public FishFarm? FishFarm_Worked { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? EndedOn { get; set; }

    }
}
