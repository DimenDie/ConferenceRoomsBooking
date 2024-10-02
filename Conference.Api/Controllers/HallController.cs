namespace Conference.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HallController : ControllerBase
{
    private readonly ApplicationDbContext _context;


    public HallController(ApplicationDbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Get the hall by Id.
    /// </summary>
    [HttpGet("{hallId}")]
    public async Task<IActionResult> GetHallById(int hallId)
    {
        var hall = await _context.Halls.FindAsync(hallId);
        if (hall == null)
            return NotFound();

        return Ok(hall);
    }

    /// <summary>
    /// Get the list of all halls.
    /// </summary>
    [HttpGet]
    public IActionResult GetAll()
    {
        var halls = _context.Halls.ToList();

        if (halls == null)
            return NotFound();
        return Ok(halls);
    }


    /// <summary>
    /// Add a hall using JSON payload.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> AddHall([FromBody] HallDto hallDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await CreateHall(hallDto);
        return Ok();
    }

    private async Task CreateHall([FromBody] HallDto hallDto)
    {
        var hall = new Hall
        {
            Name = hallDto.Name,
            Capacity = hallDto.Capacity,
            RatePerHour = hallDto.RatePerHour
        };

        var services = hallDto.Services.Select(s => new Service
        {
            Name = s.Name,
            Price = s.Price,
            HallNavigation = hall
        }).ToList();

        hall.Services = services;

        _context.Halls.Add(hall);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Update the hall by Id using JSON payload.
    /// </summary>
    [HttpPut("{hallId}")]
    public async Task<IActionResult> UpdateHall(int hallId, UpdateHallDto updateHallDto)
    {
        var hall = _context.Halls.Include(h => h.Services).FirstOrDefault(h => h.Id == hallId);

        if (hall == null)
            return NotFound();
        hall.Name = updateHallDto.Name;
        hall.Capacity = updateHallDto.Capacity;
        hall.RatePerHour = updateHallDto.RatePerHour;

        if (updateHallDto.ServiceDtos != null)
        {
            foreach (var item in updateHallDto.ServiceDtos)
            {
                var serviceToAdd = new Service
                {
                    Name = item.Name,
                    Price = item.Price,
                };
                hall.Services.Add(serviceToAdd);
            }
        }
        await _context.SaveChangesAsync();
        return NoContent();
    }


    /// <summary>
    /// Delete the hall by Id.
    /// </summary>
    [HttpDelete]
    [Route("{hallId}")]
    public IActionResult Delete([FromRoute] int hallId)
    {
        var hall = _context.Halls.FirstOrDefault(i => i.Id == hallId);

        if (hall == null)
            return NotFound();

        _context.Remove(hall);
        _context.SaveChanges();
        return NoContent();
    }
}
