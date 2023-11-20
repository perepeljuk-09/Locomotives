using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locomotives.Domain.Entities
{
    public class LocomotiveCategories
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public List<Locomotive>? Locomotives { get; set; }
        public List<LocomotiveCategoriesDrivers>? LocomotiveCategoriesDrivers { get; set; }
    }
    public class ConfigurationLocomotiveCategories : IEntityTypeConfiguration<LocomotiveCategories>
    {
        public void Configure(EntityTypeBuilder<LocomotiveCategories> builder)
        {
            builder.ToTable("locomotive_categories", "main");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("locomotive_category_id").IsRequired();
            builder.Property(x => x.CategoryName).HasColumnName("category_name");

        }
    }
}
