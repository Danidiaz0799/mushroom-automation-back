namespace Domain.Entities
{
    public class DhtSensor
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
    }
}
