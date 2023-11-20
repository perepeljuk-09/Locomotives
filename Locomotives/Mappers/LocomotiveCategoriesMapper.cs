using Locomotives.API.Models.Dto.LocomotiveCategories;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Mappers
{
    public class LocomotiveCategoriesMapper
    {
        public static LocomotiveCategoriesDto ToDto(LocomotiveCategories locoCategories)
        {
            LocomotiveCategoriesDto dto = new()
            {
                Id = locoCategories.Id,
                CategoryName = locoCategories.CategoryName,
            };
            return dto;
        }
        public static LocomotiveCategories ToDomain(LocomotiveCategoriesCreateDto dto)
        {
            LocomotiveCategories category = new()
            {
                CategoryName = dto.CategoryName,
            };
            return category;
        }
        public static LocomotiveCategories ToDomain(int id, LocomotiveCategoriesUpdateDto dto)
        {
            LocomotiveCategories category = new()
            {
                Id = id,
                CategoryName = dto.CategoryName,
            };
            return category;
        }
    }
}
