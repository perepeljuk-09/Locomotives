namespace Locomotives.API.Models.Dto.Driver
{
    public class DriverCreateDto
    {
        public string? FirstName { get; set; }
        public int LocomotiveId { get; set; }
        public List<int>? CategoriesIds { get; set; }
    }
}
