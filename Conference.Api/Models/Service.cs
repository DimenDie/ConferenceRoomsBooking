namespace Conference.Api.Models;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public decimal RatePerHour { get; set; }

    public int HallId { get; set; }
    public Hall HallNavigation { get; set; } = null!;

    public Booking? BookingNavigation { get; set; }
}
