namespace Locomotives.API.Models.Dto.Driver
{
    public class DriverDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public bool IsVacation { get; set; }
        public int LocomotiveId { get; set; }
        public Dictionary<int, string>? LocoCategories { get; set; }
    }
}
