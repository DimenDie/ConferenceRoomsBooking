public class BookingDto
{
    public int HallId { get; set; }

    public DateTime StartTime { get; set; }
    public int Duration { get; set; }

    public IEnumerable<Service>? Services = new List<Service>();
}
