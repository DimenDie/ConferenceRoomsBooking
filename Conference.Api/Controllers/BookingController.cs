[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("Book")]
    public async Task<IActionResult> BookHall([FromBody] BookingDto request)
    {
        var hall = await _context.Halls.FindAsync(request.HallId);
        if (hall == null)
        {
            return NotFound();
        }

        var services = hall.Services;

        decimal modifiedRatePerHour = hall.RatePerHour;
        DateTime bookingEndTime = request.StartTime.AddHours(request.Duration);

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
    }
    [HttpGet]
    public Task<IActionResult> FindAvailableHalls([FromBody] BookingDto request)
    {
        throw new NotImplementedException();
    }
}
