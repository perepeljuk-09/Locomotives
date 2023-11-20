namespace Locomotives.API.Models.Dto.Depot
{
    public class DepotDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Dictionary<int, string>? Locomotives { get; set; } = new();
    }
}
