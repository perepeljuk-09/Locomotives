namespace Locomotives.API.Models.Dto.Driver
{
    public class DriverUpdateDto
    {
        public string? FirstName { get; set; }
        public bool IsVacation { get; set; }
        public int LocomotiveId { get; set; }
        public List<int>? CategoriesIds { get; set; }
    }
}
