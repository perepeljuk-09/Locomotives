using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locomotives.Domain.Entities
{
    public class Locomotive
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public int? DepotId { get; set; }
        public Depot? Depot { get; set; }
        public List<Driver>? Drivers { get; set; }
        public int LocomotiveCategoryId { get; set; }
        public LocomotiveCategories? LocomotiveCategories { get; set; }
    }
    public class ConfigurationLocomotive : IEntityTypeConfiguration<Locomotive>
    {
        public void Configure(EntityTypeBuilder<Locomotive> builder)
        {
            builder.ToTable("locomotives", "main");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("locomotive_id");
            builder.Property(x => x.Name).HasColumnName("locomotive_name");
            builder.Property(x => x.ReleaseDate).HasColumnName("release_date");
            builder.Property(x => x.DepotId).HasColumnName("depot_id");
            builder.Property(x => x.LocomotiveCategoryId).HasColumnName("locomotive_category_id").IsRequired();

            builder.HasOne(x => x.Depot).WithMany(x => x.Locomotives).HasForeignKey(x => x.DepotId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.LocomotiveCategories).WithMany(x => x.Locomotives).HasForeignKey(x => x.LocomotiveCategoryId);
        }
    }
}

