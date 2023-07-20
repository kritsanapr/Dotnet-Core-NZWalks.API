using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SQLRegionRepository : IRegionRepository
{
    private readonly NZWalkDbContext _context;
    public SQLRegionRepository(NZWalkDbContext context)
    {
        _context = context;
    }

    public NZWalkDbContext Context { get; }

    public async Task<List<Region>> GetAllAsync()
    {
        return await _context.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id)
    {
        var region = await _context.Regions.FirstOrDefaultAsync(x=>x.Id == id);
      
        return region;
    }

    public async Task<Region> CreateAsync(Region region)
    {
        await _context.Regions.AddAsync(region);
        await _context.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existing = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existing != null)
        {
            return null;
        }

        existing.Code = region.Code;
        existing.Name = region.Name;
        existing.RegionImageUrl  = region.RegionImageUrl;

        await _context.SaveChangesAsync();

        return region;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingRegion = await _context.Regions.FirstOrDefaultAsync(x=> x.Id == id);
        if (existingRegion == null)
        {
            return null;
        }

        _context.Regions.Remove(existingRegion);
        await _context.SaveChangesAsync();

        return existingRegion;
    }
}
