using Locomotives.API.Models.Dto.Locomotive;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Services.Contracts
{
    public interface ILocomotivesService : IService<Locomotive>
    {
        Task<List<LocomotiveDto>> GetAllAsync();
        Task<LocomotiveDto?> GetByIdAsync(int id);
        Task<LocomotiveDto?> CreateAsync(LocomotiveCreateDto dto, List<string> errors);
        Task<LocomotiveDto?> UpdateAsync(int id, LocomotiveUpdateDto dto, List<string> errors);
    }
}
