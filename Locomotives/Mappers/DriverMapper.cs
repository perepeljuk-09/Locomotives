using Locomotives.API.Models.Dto.Driver;
using Locomotives.Domain.Entities;

namespace Locomotives.API.Mappers
{
    public class DriverMapper
    {
        public static DriverDto ToDto(Driver driver)
        {
            DriverDto driverDto = new DriverDto()
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                IsVacation = driver.IsVacation,
                LocomotiveId = driver.LocomotiveId,
                LocoCategories = driver.LocomotiveCategoriesDrivers
                    .ToDictionary(x => x.LocomotiveCategoryId, x => x.LocomotiveCategory.CategoryName)
            };
            return driverDto;
        }
        public static Driver ToDomain(DriverCreateDto dto)
        {
            Driver driver = new Driver()
            {
                FirstName = dto.FirstName,
                LocomotiveId = dto.LocomotiveId,
                LocomotiveCategoriesDrivers = dto.CategoriesIds
                    .Select(x => new LocomotiveCategoriesDrivers()
                    {
                        LocomotiveCategoryId = x
                    }).ToList()
            };

            return driver;
        }
        public static Driver ToDomain(DriverUpdateDto dto, List<int> LocoCategories)
        {
            Driver driver = new Driver();

            driver.IsVacation = dto.IsVacation;
            driver.LocomotiveId = dto.LocomotiveId;
            driver.FirstName = dto.FirstName;

            driver.LocomotiveCategoriesDrivers = dto.CategoriesIds
                .Where(x => LocoCategories.Contains(x))
                .Select(x => new LocomotiveCategoriesDrivers()
                {
                    LocomotiveCategoryId = x
                }).ToList();

            return driver;
        }
    }
}
