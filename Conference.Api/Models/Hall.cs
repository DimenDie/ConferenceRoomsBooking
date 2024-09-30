namespace Conference.Api.Models;

public class Hall
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }
    public decimal RatePerHour { get; set; }

    public List<Service>? Services { get; set; } = new List<Service>();

    public List<Booking>? Bookings { get; set; } = new List<Booking>();
}


