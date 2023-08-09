using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class SQLWalkRepository : IWalkRepository
{
    private readonly NZWalkDbContext _context;
    public SQLWalkRepository(NZWalkDbContext context)
    {
        _context = context;
    }


    public async Task<Walk> CreateAsync(Walk walk)
    {
        await _context.Walks.AddAsync(walk);
        await _context.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetAllAsync()
    {
        return await _context.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public async Task<Walk?> GetByIdAsync(Guid id)
    {
        return await _context.Walks
            .Include("Difficulty")
            .Include("Region")
            .FirstOrDefaultAsync(x => x.Id == id);
    }
        
}
