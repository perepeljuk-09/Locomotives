using Locomotives.API.Models.Dto.Locomotive;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Mappers
{
    public class LocomotiveMapper
    {
        public static LocomotiveDto ToDto(Locomotive locomotive)
        {
            LocomotiveDto dto = new()
            {
                Id = locomotive.Id,
                Name = locomotive.Name,
                ReleaseDate = locomotive.ReleaseDate,
                DepotId = locomotive.DepotId,
                DepotName = locomotive.Depot?.Name,
                LocomotiveCategoryId = locomotive.LocomotiveCategoryId,
                LocomotiveCategoryName = locomotive.LocomotiveCategories.CategoryName,
                Drivers = locomotive.Drivers.ToDictionary(d => d.Id, d => d.FirstName)
            };
            return dto;
        }
        public static Locomotive ToDomain(LocomotiveCreateDto locoDto)
        {
            Locomotive locomotive = new()
            {
                Name = locoDto.Name,
                ReleaseDate = locoDto.ReleaseDate,
                LocomotiveCategoryId = locoDto.CategoryId,
                DepotId = locoDto.DepotId
            };
            return locomotive;
        }
        public static Locomotive ToDomain(int id, LocomotiveUpdateDto locoDto)
        {
            Locomotive locomotive = new()
            {
                Id = id,
                Name = locoDto.Name,
                ReleaseDate = locoDto.ReleaseDate,
                LocomotiveCategoryId = locoDto.CategoryId,
                DepotId = locoDto.DepotId
            };
            return locomotive;

        }
    }
}
