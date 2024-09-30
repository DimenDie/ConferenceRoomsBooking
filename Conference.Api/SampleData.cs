public static class SampleData
{
    public static List<Hall> Halls => new()
    {
        new () {Name = "Hall A", Capacity = 50, RatePerHour = 2000},
        new () {Name = "Hall B", Capacity = 100, RatePerHour = 3500},
        new () {Name = "Hall C", Capacity = 30, RatePerHour = 1500}
    };

    public static List<Service> Services => new()
    {
        new() {Name = "Projector", Price = 500, HallId = 1},
        new() {Name = "Wi-Fi", Price = 300, HallId = 2},
        new() {Name = "Sound", Price = 700, HallId = 3}
    };

    public static void InitializeData(ApplicationDbContext context)
    {
        DropAndCreateDatabase(context);
        SeedData(context);
    }

    private static void DropAndCreateDatabase(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.Migrate();
        context.Database.EnsureCreated();
    }

    private static void SeedData(ApplicationDbContext context)
    {
        context.Halls.AddRange(Halls);
        context.Services.AddRange(Services);
        context.SaveChanges();
    }
}
