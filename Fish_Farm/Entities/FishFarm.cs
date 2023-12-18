using Azure.Core.GeoJson;
using System.Drawing;

namespace Fish_Farm.Entities
{
    public class FishFarm
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Num_of_cages {  get; set; }
        public string? Image {  get; set; }
        public Boolean Has_barge { get; set; }
        public ICollection<Worker> Workers { get; set; }

    }
}