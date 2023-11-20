using Locomotives.API.Models.Dto.LocomotiveCategories;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Services.Contracts
{
    public interface ILocomotiveCategoriesService : IService<LocomotiveCategories>
    {
        Task<List<LocomotiveCategoriesDto>> GetAllAsync();
        Task<LocomotiveCategoriesDto?> GetByIdAsync(int id);
        Task<LocomotiveCategoriesDto> CreateAsync(LocomotiveCategoriesCreateDto dto);
        Task<LocomotiveCategoriesDto?> UpdateAsync(int id, LocomotiveCategoriesUpdateDto dto);
    }
}
