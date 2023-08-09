using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Runtime.InteropServices;

namespace NZWalks.API.Controllers;

// https://localhost:port/api/regions
[Route("api/[controller]")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly NZWalkDbContext _context;
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionController(NZWalkDbContext context, IRegionRepository regionRepository, IMapper mapper)
    {
        _context = context;
        _regionRepository = regionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        // Get data from database - domain models
       var regionsDomain = await _regionRepository.GetAllAsync();

        // Map domain models to DTOs
        var regionsDto = _mapper.Map<List<RegionDto>>(regionsDomain);
        
        // Return DTOs
        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var regionDomain = await _regionRepository.GetByIdAsync(id);
        if(regionDomain == null)
        {
            return NotFound();
        }


        
        var regionsDto = _mapper.Map<RegionDto>(regionDomain);

        return Ok(regionsDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] AddRegionRequestDto addRegionRequestDto) {
        var regionDomainModel = new Region()
        {
            Code = addRegionRequestDto.Code,
            Name = addRegionRequestDto.Name,
            RegionImageUrl = addRegionRequestDto.RegionImageUrl,
        };

       regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

        var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

        return CreatedAtAction(nameof(GetByIdAsync), new {id = regionDomainModel.Id}, regionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest) 
    {
        // Map DTO to Domain model
        var regionDomainModel = _mapper.Map<Region>(updateRegionRequest);

        regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

        if (regionDomainModel == null)
            return NotFound();

        // Convert Domain Model to DTO

        return Ok(_mapper.Map<RegionDto>(regionDomainModel)); 
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var regionDomainModel = await _regionRepository.DeleteAsync(id);

        if (regionDomainModel == null)
            return NotFound();

        return Ok(_mapper.Map<RegionDto>(regionDomainModel));
    }
}
