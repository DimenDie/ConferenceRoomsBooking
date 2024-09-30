public class UpdateHallDto
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public decimal RatePerHour { get; set; }
    public List<ServiceDto>? ServiceDtos { get; set; }
}
