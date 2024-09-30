namespace Conference.Api.Models;
public class Booking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public IEnumerable<Service>? Services = new List<Service>();

    public int HallId { get; set; }
    public Hall HallNavigation { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public int Duration { get; set; }

    public decimal TotalPrice { get; set; }
}
