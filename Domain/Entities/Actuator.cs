namespace Domain.Entities
{
    public class Actuator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }
        public DateTime Timestamp { get; set; }
    }
}