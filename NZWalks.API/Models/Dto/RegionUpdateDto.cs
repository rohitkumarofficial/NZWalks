namespace NZWalks.API.Models.Dto
{
    public class RegionUpdateDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
