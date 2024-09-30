public class HallDto
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public decimal RatePerHour { get; set; }
    public List<ServiceDto> Services { get; set; } = new List<ServiceDto>();
}
