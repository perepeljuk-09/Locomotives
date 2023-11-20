namespace Locomotives.API.Models.Dto.Locomotive
{
    public class LocomotiveUpdateDto
    {
        public string? Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int DepotId { get; set; }
        public int CategoryId { get; set; }
    }
}
