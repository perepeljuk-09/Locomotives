using Locomotives.API.Mappers;
using Locomotives.API.Models.Dto.Locomotive;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Locomotives.Infrastructure.DataBases;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Locomotives.API.Services
{
    public class LocomotivesService : ILocomotivesService
    {
        public async Task<List<LocomotiveDto>> GetAllAsync()
        {
            using PgContext db = new PgContext();

            List<Locomotive> locomotives = await db.Locomotives
                .Include(x => x.Drivers)
                .Include(x => x.Depot)
                .Include(x => x.LocomotiveCategories)
                .ToListAsync();

            List<LocomotiveDto> dtos = locomotives.Select(l => LocomotiveMapper.ToDto(l)).ToList();

            return dtos;
        }
        public async Task<LocomotiveDto?> GetByIdAsync(int id)
        {
            using PgContext db = new PgContext();

            Locomotive locomotive = await db.Locomotives
                .Include(x => x.Drivers)
                .Include(x => x.Depot)
                .Include(x => x.LocomotiveCategories)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (locomotive == null)
                return null;

            LocomotiveDto dto = LocomotiveMapper.ToDto(locomotive);

            return dto;
        }
        public async Task<LocomotiveDto?> CreateAsync(LocomotiveCreateDto dto, List<string> errors)
        {
            using PgContext db = new PgContext();

            List<int> depotIds = db.Depots.Select(x => x.Id).ToList();
            List<int> locoCategoriesIds = db.LocomotiveCategories.Select(x => x.Id).ToList();

            if (!depotIds.Contains(dto.DepotId))
            {
                // throw error DepotId without range
                errors.Add($"error Depot with id equals {dto.DepotId} without range");
            }
            if (!locoCategoriesIds.Contains(dto.CategoryId))
            {
                // throw error categoryID without range
                errors.Add($"error LocoCategory with id equals {dto.CategoryId} without range");
            }

            if (errors.Count > 0)
                return null;

            Locomotive locomotive = LocomotiveMapper.ToDomain(dto);

            await db.AddAsync(locomotive);
            await db.SaveChangesAsync();

            locomotive = await db.Locomotives
                .Include(x => x.Drivers)
                .Include(x => x.Depot)
                .Include(x => x.LocomotiveCategories)
                .FirstOrDefaultAsync(x => x.Id == locomotive.Id);

            if (locomotive == null)
                return null;

            LocomotiveDto locoDto = LocomotiveMapper.ToDto(locomotive);
            
            return locoDto;
        }
        public async Task<LocomotiveDto?> UpdateAsync(int id, LocomotiveUpdateDto dto, List<string> errors)
        {
            using PgContext db = new PgContext();

            List<int> depotIds = db.Depots.Select(x => x.Id).ToList();
            List<int> locoCategoriesIds = db.LocomotiveCategories.Select(x => x.Id).ToList();

            if (!depotIds.Contains(dto.DepotId))
            {
                // throw error DepotId without range
                errors.Add($"error Depot with id equals {dto.DepotId} without range");
            }
            if (!locoCategoriesIds.Contains(dto.CategoryId))
            {
                // throw error categoryID without range
                errors.Add($"error LocoCategory with id equals {dto.CategoryId} without range");
            }

            if (errors.Count > 0)
                return null;


            Locomotive locomotiveForUpdate = await db.Locomotives
                .FirstOrDefaultAsync(x => x.Id == id);

            if (locomotiveForUpdate == null)
                return null;

            locomotiveForUpdate.Name = dto.Name;
            locomotiveForUpdate.LocomotiveCategoryId = dto.CategoryId;
            locomotiveForUpdate.ReleaseDate = dto.ReleaseDate;
            locomotiveForUpdate.DepotId = dto.DepotId;

            db.Update(locomotiveForUpdate);
            await db.SaveChangesAsync();

            Locomotive locomotive = await db.Locomotives
                .Include(x => x.Drivers)
                .Include(x => x.Depot)
                .Include(x => x.LocomotiveCategories)
                .FirstOrDefaultAsync(x => x.Id == id);
            // error was here
            LocomotiveDto locoDto = LocomotiveMapper.ToDto(locomotive);

            return locoDto;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using PgContext db = new PgContext();

            Locomotive locomotive = await db.Locomotives.FirstOrDefaultAsync(x => x.Id == id);

            if (locomotive == null)
                return false;

            db.Remove(locomotive);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
