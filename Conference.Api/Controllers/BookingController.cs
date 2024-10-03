[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Add a booking using JSON payload.
    /// </summary>
    [HttpPost("Book")]
    public async Task<IActionResult> BookHall([FromBody] BookingDto request)
    {
        var hall = await _context.Halls.FindAsync(request.HallId);
        if (hall == null)
        {
            return NotFound();
        }

        var services = hall.Services;

        DateTime bookingEndTime = request.StartTime.AddHours(request.Duration);
        decimal modifiedRatePerHour = ModifyByBookingTime(bookingEndTime);

        decimal servicesCost = services.Sum(s => s.Price);
        decimal totalPrice = modifiedRatePerHour * request.Duration + servicesCost;

        var booking = new Booking
        {
            HallId = request.HallId,
            StartTime = request.StartTime,
            Duration = request.Duration,
            Services = request.Services,
            TotalPrice = totalPrice
        };

        _context.Bookings.Add(booking);
        _context.SaveChanges();

        _context.Bookings.Update(booking);
        _context.SaveChanges();

        return Ok(new { BookingId = booking.Id, TotalPrice = totalPrice });

        decimal ModifyByBookingTime(DateTime bookingEndTime)
        {
            decimal modifiedRatePerHour = hall.RatePerHour;

            if (request.StartTime.Hour >= 18 && bookingEndTime.Hour <= 23)
            {
                modifiedRatePerHour *= 0.8m;
            }
            else if (request.StartTime.Hour >= 6 && bookingEndTime.Hour <= 9)
            {
                modifiedRatePerHour *= 0.9m;
            }
            else if (request.StartTime.Hour >= 12 && bookingEndTime.Hour <= 14)
            {
                modifiedRatePerHour *= 1.15m;
            }

            return modifiedRatePerHour;
        }
    }

    /// <summary>
    /// Get the list of all currently available halls
    /// </summary>
    [HttpGet]
    public IActionResult FindAvailableHalls(
        [FromQuery] string startTimeString = "2024-10-03T08:11:00Z",
        [FromQuery] int duration = 1,
        [FromQuery] int capacity = 1,
        [FromQuery] List<int>? serviceIds = null)
    {
        var startTime = DateTime.Parse(startTimeString);
        var hallsWithSpecifiedServicesAndCapacity = _context.Halls
            .Where(h => h.Capacity >= capacity
                        && (serviceIds == null || serviceIds.All(s => h.Services.Any(service => service.Id == s))));

        var hallIdsAvailableAtTime = _context.Bookings
            .Where(b => startTime < b.StartTime.AddHours(b.Duration)
                        && b.StartTime < startTime.AddHours(duration))
            .Select(b => b.HallId);

        var fullyAvailableHalls = hallsWithSpecifiedServicesAndCapacity
            .Where(h => !hallIdsAvailableAtTime.Contains(h.Id));

        return Ok(fullyAvailableHalls);
    }
}
