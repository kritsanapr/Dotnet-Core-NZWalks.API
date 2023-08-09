namespace NZWalks.API.Models.DTO;

public class AddWalksRequestDto
{
    public string Name { get; set; } = default!;
    public string description { get; set; } = default!;
    public double LenghInKm { get; set; }
    public string? WalkImageUrl { get; set; }

    public Guid DifficultyId { get; set; }
    public Guid RegionId { get; set; }
}
