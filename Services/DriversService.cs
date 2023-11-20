using Locomotives.API.Mappers;
using Locomotives.API.Models.Dto.Driver;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Locomotives.Infrastructure.DataBases;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Locomotives.API.Services
{
    public class DriversService : IDriversService
    {
        public async Task<List<DriverDto>> GetAllAsync()
        {
            using PgContext db = new PgContext();

            List<Driver> driversDb = await db.Drivers
                .Include(x => x.LocomotiveCategoriesDrivers)
                .ThenInclude(x => x.LocomotiveCategory)
                .AsNoTracking()
                .ToListAsync();


            var drivers = driversDb.Select(x => DriverMapper.ToDto(x)).ToList();

            return drivers;
        }
        public async Task<DriverDto?> GetByIdAsync(int id)
        {
            using PgContext db = new PgContext();

            Driver? driver = await db.Drivers
                .Include(x => x.LocomotiveCategoriesDrivers)
                .ThenInclude(x => x.LocomotiveCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (driver == null)
                return null;

            DriverDto dto = DriverMapper.ToDto(driver);
            return dto;
        }
        public async Task<DriverDto?> CreateAsync(DriverCreateDto dto, List<string> errors)
        {
            using PgContext db = new PgContext();

            List<int> LocoCategories = await db.LocomotiveCategories.Select(x => x.Id).ToListAsync();

            List<int> locoIds = await db.Locomotives.Select(x => x.Id).ToListAsync();

            Locomotive locomotive = await db.Locomotives
                .FirstOrDefaultAsync(x => x.Id == dto.LocomotiveId);

            if (dto.CategoriesIds.Any(id => !LocoCategories.Contains(id)))
            {
                // throw error categoryId without range
                errors.Add("categoryId without range");
            }

            if (!locoIds.Contains(dto.LocomotiveId))
            {
                // throw error locomotiveId without range
                errors.Add("locomotiveId without range");
            }
            else
            {
                if (!dto.CategoriesIds.Contains(locomotive!.LocomotiveCategoryId))
                {
                    // throw error if driver can't drive this locomotive because haven't needless category
                    errors.Add($"The driver haven't category for this locomotive, locomotiveCategoryId is {locomotive.LocomotiveCategoryId}");
                }
            }

            if (errors.Count > 0)
                return null;

            Driver driver = DriverMapper.ToDomain(dto);

            await db.Drivers.AddAsync(driver);
            await db.SaveChangesAsync();

            Driver driverFromDb = await db.Drivers
                .Include(x => x.LocomotiveCategoriesDrivers)
                .ThenInclude(x => x.LocomotiveCategory)
                .FirstOrDefaultAsync(x => x.Id == driver.Id);

            if (driverFromDb == null)
                return null;

            DriverDto driverDto = DriverMapper.ToDto(driverFromDb);

            return driverDto;
        }
        public async Task<DriverDto?> UpdateAsync(int id, DriverUpdateDto dto, List<string> errors)
        {
            using PgContext db = new PgContext();

            List<int> LocoCategories = await db.LocomotiveCategories.Select(x => x.Id).ToListAsync();

            List<int> locoIds = await db.Locomotives.Select(x => x.Id).ToListAsync();

            Locomotive locomotive = await db.Locomotives
                .Include(x => x.LocomotiveCategories)
                .FirstOrDefaultAsync(x => x.Id == dto.LocomotiveId);

            if (dto.CategoriesIds.Any(id => !LocoCategories.Contains(id)))
            {
                // throw error categoryId without range
                errors.Add("categoryId without range");
            }

            if (!locoIds.Contains(dto.LocomotiveId))
            {
                // throw error locId without range
                errors.Add("locomotiveId without range");
            }
            else
            {
                if (!dto.CategoriesIds.Contains(locomotive!.LocomotiveCategoryId))
                {
                    // throw error if driver can't drive this locomotive because haven't needless category
                    errors.Add($"The driver haven't category for this locomotive, locomotiveCategoryId is {locomotive.LocomotiveCategoryId}");
                }
            }

            if (errors.Count > 0)
                return null;

            Driver? driverFinded = await db.Drivers
                .Include(x => x.LocomotiveCategoriesDrivers)
                .ThenInclude(x => x.LocomotiveCategory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (driverFinded == null)
                return null;

            var locoCategories = await db.LocomotiveCategories.Select(x => x.Id).ToListAsync();

            driverFinded.FirstName = dto.FirstName;
            driverFinded.IsVacation = dto.IsVacation;
            driverFinded.LocomotiveId = dto.LocomotiveId;
            driverFinded.LocomotiveCategoriesDrivers = dto.CategoriesIds
                .Where(x => LocoCategories.Contains(x))
                .Select(x => new LocomotiveCategoriesDrivers()
                {
                    LocomotiveCategoryId = x
                }).ToList();


            db.Drivers.Update(driverFinded);
            await db.SaveChangesAsync();

            /*await db.Entry(driverFinded).Collection(x => x.LocomotiveCategoriesDrivers).LoadAsync();

            foreach (var item in driverFinded.LocomotiveCategoriesDrivers)
            {
                await db.Entry(item).Reference(x => x.LocomotiveCategory).LoadAsync();
            }*/

            driverFinded = await db.Drivers
            .Include(x => x.LocomotiveCategoriesDrivers)
            .ThenInclude(x => x.LocomotiveCategory)
            .FirstOrDefaultAsync(x => x.Id == id);

            if (driverFinded == null)
                return null;

            DriverDto driverDto = DriverMapper.ToDto(driverFinded);

            return driverDto;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using PgContext db = new PgContext();

            Driver? driver = await db.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            if (driver == null)
                return false;

            db.Remove(driver);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
