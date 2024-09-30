namespace Conference.Api.Models;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public decimal Price { get; set; }

    public int HallId { get; set; }
    public Hall HallNavigation { get; set; } = null!;
}
