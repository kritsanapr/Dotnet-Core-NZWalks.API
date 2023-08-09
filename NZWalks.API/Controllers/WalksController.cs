using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;
    public WalksController(IMapper mapper, IWalkRepository walkRepository)
    {
        _mapper = mapper;
        _walkRepository = walkRepository;
    }


    // CREATE Walk
    // POST : /api/walks
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto) {
        // Map DTO to Domain Model
        var walkDomainModel = _mapper.Map<Walk>(addWalksRequestDto);

        await _walkRepository.CreateAsync(walkDomainModel);

        return Ok(_mapper.Map<WalkDto>(walkDomainModel));
    }

    // GET Walks
    // GET : /api/walks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var walksDomainModel = await _walkRepository.GetAllAsync();

        return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var walkDomainModel = _walkRepository.GetByIdAsync(id);
        if (walkDomainModel == null)
        {
            return NotFound();
        }

        var walkDto = _mapper.Map<WalkDto>(walkDomainModel);
        return Ok(walkDto);
    }
}
