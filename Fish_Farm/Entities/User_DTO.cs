namespace Fish_Farm.Entities
{
    public class User_DTO
    {
        public string Name { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public string? Image { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
