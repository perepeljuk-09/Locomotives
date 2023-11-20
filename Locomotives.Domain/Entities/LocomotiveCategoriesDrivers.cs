using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locomotives.Domain.Entities
{
    public class LocomotiveCategoriesDrivers
    {
        public int DriverId { get; set; }
        public Driver? Driver { get; set; }
        public int LocomotiveCategoryId { get; set; }
        public LocomotiveCategories? LocomotiveCategory { get; set; }
    }
    public class ConfigurationLocomotiveCategoriesDrivers : IEntityTypeConfiguration<LocomotiveCategoriesDrivers>
    {
        void IEntityTypeConfiguration<LocomotiveCategoriesDrivers>.Configure(EntityTypeBuilder<LocomotiveCategoriesDrivers> builder)
        {
            builder.ToTable("locomotive_categories_drivers", "main");
            builder.HasKey(x => new { x.LocomotiveCategoryId , x.DriverId});
            builder.Property(x => x.DriverId).HasColumnName("driver_id").IsRequired();
            builder.Property(x => x.LocomotiveCategoryId).HasColumnName("locomotive_category_id").IsRequired();

            builder.HasOne(x => x.Driver).WithMany(x => x.LocomotiveCategoriesDrivers).HasForeignKey(x => x.DriverId);
            builder.HasOne(x => x.LocomotiveCategory).WithMany(x => x.LocomotiveCategoriesDrivers).HasForeignKey(x => x.LocomotiveCategoryId);
        }
    }
}
