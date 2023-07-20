namespace NZWalks.API.Models.Domain;

public class Walk
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string description { get; set; } = default!;
    public double LenghInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    
    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }

    // Navigation properties 
    public Difficulty Difficulty { get; set; } = default!;
    public Region Region { get; set; } = default!;

}
