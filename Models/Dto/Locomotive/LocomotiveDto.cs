namespace Locomotives.API.Models.Dto.Locomotive
{
    public class LocomotiveDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int? DepotId { get; set; }
        public string? DepotName { get; set; }
        public Dictionary<int, string>? Drivers { get; set; }
        public int LocomotiveCategoryId { get; set; }
        public string? LocomotiveCategoryName { get; set; }
    }
}
