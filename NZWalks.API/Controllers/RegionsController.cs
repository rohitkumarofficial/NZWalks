using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public RegionsController(NZWalksDbContext nZWalksDbContext)
        {
            _nZWalksDbContext = nZWalksDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetAll()
        {
            var regions = await _nZWalksDbContext.Regions.ToListAsync();

            return Ok(regions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegionById(Guid id)
        {
            var region = await _nZWalksDbContext.Regions.FindAsync(id);
            if (region == null) { return NotFound(); }
            return Ok(region);
        }

        [HttpPost]
        public async Task<ActionResult<Region>> AddRegion([FromBody] RegionCreateDto regionCreateDto)
        {
            // Map DTO to domain Model
            var regionDomainModel = new Region
            {
                Code = regionCreateDto.Code,
                Name = regionCreateDto.Name,
                RegionImageUrl = regionCreateDto.RegionImageUrl
            };

            _nZWalksDbContext.Regions.Add(regionDomainModel);
            await _nZWalksDbContext.SaveChangesAsync();
            return Ok(regionDomainModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Region>> UpdateRegion([FromRoute] Guid id, [FromBody] RegionUpdateDto regionDto)
        {
            var regionDomainModel = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);
            if (regionDomainModel == null) { return BadRequest("Region Doesn't exits in the database"); }

            regionDomainModel.Code = regionDto.Code;
            regionDomainModel.Name = regionDto.Name;
            regionDomainModel.RegionImageUrl = regionDto.RegionImageUrl;

            _nZWalksDbContext.Regions.Update(regionDomainModel);
            await _nZWalksDbContext.SaveChangesAsync();
            return Ok(regionDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var region = await _nZWalksDbContext.Regions.FirstOrDefaultAsync(region => region.Id == id);

            if (region == null) { return NotFound(); }

            _nZWalksDbContext.Regions.Remove(region);
            await _nZWalksDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
