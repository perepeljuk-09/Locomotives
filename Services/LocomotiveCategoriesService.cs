using Locomotives.API.Mappers;
using Locomotives.API.Models.Dto.LocomotiveCategories;
using Locomotives.API.Services.Contracts;
using Locomotives.Domain.Entities;
using Locomotives.Infrastructure.DataBases;
using Microsoft.EntityFrameworkCore;

namespace Locomotives.API.Services
{
    public class LocomotiveCategoriesService : ILocomotiveCategoriesService
    {
        public async Task<List<LocomotiveCategoriesDto>> GetAllAsync()
        {
            using PgContext db = new PgContext();

            List<LocomotiveCategories> locoCategories = await db.LocomotiveCategories.ToListAsync();

            List<LocomotiveCategoriesDto> dtos = locoCategories.Select(c => LocomotiveCategoriesMapper.ToDto(c)).ToList();

            return dtos;
        }
        public async Task<LocomotiveCategoriesDto?> GetByIdAsync(int id)
        {
            using PgContext db = new PgContext();

            LocomotiveCategories locoCategory = await db.LocomotiveCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (locoCategory == null)
                return null;

            LocomotiveCategoriesDto dto = LocomotiveCategoriesMapper.ToDto(locoCategory);

            return dto;
        }
        public async Task<LocomotiveCategoriesDto> CreateAsync(LocomotiveCategoriesCreateDto dto)
        {
            using PgContext db = new PgContext();

            LocomotiveCategories category = LocomotiveCategoriesMapper.ToDomain(dto);

            await db.AddAsync(category);
            await db.SaveChangesAsync();

            LocomotiveCategoriesDto locoDto = LocomotiveCategoriesMapper.ToDto(category);

            return locoDto;
        }
        public async Task<LocomotiveCategoriesDto?> UpdateAsync(int id, LocomotiveCategoriesUpdateDto dto)
        {
            using PgContext db = new PgContext();

            LocomotiveCategories category = await db.LocomotiveCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return null;

            //category = LocomotiveCategoriesMapper.ToDomain(id, dto);
            category.CategoryName = dto.CategoryName;

            db.Update(category);
            await db.SaveChangesAsync();

            LocomotiveCategoriesDto locoDto = LocomotiveCategoriesMapper.ToDto(category);

            return locoDto;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using PgContext db = new PgContext();

            LocomotiveCategories category = await db.LocomotiveCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return false;

            db.Remove(category);
            await db.SaveChangesAsync();

            return true;
        }
    }
}
