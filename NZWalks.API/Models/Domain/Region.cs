namespace NZWalks.API.Models.Domain;

public class Region
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? RegionImageUrl { get; set; } = default!;
}
