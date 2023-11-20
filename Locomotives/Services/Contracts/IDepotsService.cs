using Locomotives.API.Models.Dto.Depot;
using Locomotives.API.Models.Dto.Driver;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Services.Contracts
{
    public interface IDepotsService : IService<Depot>
    {
        Task<List<DepotDto>> GetAllAsync();
        Task<DepotDto?> GetByIdAsync(int id);
        Task<DepotDto> CreateAsync(DepotCreateDto dto);
        Task<DepotDto?> UpdateAsync(int id, DepotUpdateDto dto);
    }
}
