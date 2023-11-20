using Locomotives.API.Models.Dto.Depot;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Mappers
{
    public class DepotMapper
    {
        public static DepotDto ToDto(Depot depot)
        {
            DepotDto dto = new DepotDto()
            {
                Id = depot.Id,
                Name = depot.Name,
                Locomotives = depot.Locomotives.ToDictionary(x => x.Id, x => x.Name)
            };
            return dto;
        }
        public static Depot ToDomain(DepotCreateDto dto)
        {
            Depot depot = new Depot()
            {
                Name = dto.Name,
            };
            return depot;
        }
        public static Depot ToDomain(int id, DepotUpdateDto dto)
        {
            Depot depot = new Depot()
            {
                Id = id,
                Name = dto.Name,
            };
            return depot;
        }
    }
}
