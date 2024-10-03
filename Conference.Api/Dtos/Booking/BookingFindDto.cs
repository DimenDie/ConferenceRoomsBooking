public class BookingFindDto
{
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }

    public IEnumerable<Service>? Services { get; set; } = new List<Service>();
}
