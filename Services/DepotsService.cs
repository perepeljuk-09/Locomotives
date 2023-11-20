using Locomotives.API.Mappers;
using Locomotives.API.Models.Dto.Depot;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Locomotives.Infrastructure.DataBases;
using Microsoft.EntityFrameworkCore;

namespace Locomotives.API.Services
{
    public class DepotsService : IDepotsService
    {
        public async Task<List<DepotDto>> GetAllAsync()
        {
            using PgContext db = new PgContext();

            List<Depot> depots = await db.Depots.Include(d => d.Locomotives).ToListAsync();

            List<DepotDto> depotDtos = depots.Select(depot => DepotMapper.ToDto(depot)).ToList();

            return depotDtos;
        }
        public async Task<DepotDto?> GetByIdAsync(int id)
        {
            using PgContext db = new PgContext();

            Depot depot = await db.Depots.Include(d => d.Locomotives).FirstOrDefaultAsync(x => x.Id == id);

            if (depot == null)
                return null;

            DepotDto dto = DepotMapper.ToDto(depot);

            return dto;
        }
        public async Task<DepotDto> CreateAsync(DepotCreateDto dto)
        {
            using PgContext db = new PgContext();

            Depot depot = DepotMapper.ToDomain(dto);

            await db.AddAsync(depot);
            await db.SaveChangesAsync();

            Depot entity = await db.Depots
                .Include(x => x.Locomotives)
                .FirstOrDefaultAsync(x => x.Id == depot.Id);

            DepotDto depotDto = DepotMapper.ToDto(entity);

            return depotDto;
        }
        public async Task<DepotDto?> UpdateAsync(int id, DepotUpdateDto dto)
        {
            using PgContext db = new PgContext();

            Depot depot = await db.Depots.Include(x => x.Locomotives).FirstOrDefaultAsync(x => x.Id == id);

            if (depot == null)
                return null;

            //Depot depotForSave = DepotMapper.ToDomain(id, dto);
            depot.Name = dto.Name;

            db.Update(depot);
            await db.SaveChangesAsync();

            //Depot? depotForDto = await db.Depots.Include(x => x.Locomotives).FirstOrDefaultAsync(x => x.Id == depot.Id);
            //if (depotForDto == null)
            //    return null;

            DepotDto depotDto = DepotMapper.ToDto(depot);

            return depotDto;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using PgContext db = new PgContext();

            Depot depot = await db.Depots.FirstOrDefaultAsync(x => x.Id == id);

            if (depot == null)
                return false;

            db.Remove(depot);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
