using Locomotives.API.Models.Dto.Driver;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Services.Contracts
{
    public interface IDriversService : IService<Driver>
    {
        Task<List<DriverDto>> GetAllAsync();
        Task<DriverDto?> GetByIdAsync(int id);
        Task<DriverDto?> CreateAsync(DriverCreateDto dto, List<string> errors);
        Task<DriverDto?> UpdateAsync(int id,DriverUpdateDto dto, List<string> errors);
    }
}
