namespace Conference.Api.Models;

public class Hall
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public IEnumerable<Service>? Services { get; set; } = new List<Service>();

    public IEnumerable<Booking>? Bookings { get; set; }
}


