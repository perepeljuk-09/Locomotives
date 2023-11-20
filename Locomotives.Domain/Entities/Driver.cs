using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Locomotives.Domain.Entities
{
    public class Driver
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public bool IsVacation { get; set; }
        public int LocomotiveId { get; set; }
        public Locomotive? Locomotive { get; set; }
        public List<LocomotiveCategoriesDrivers>? LocomotiveCategoriesDrivers { get; set; }
    }
    public class ConfigurationDriver : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("drivers", "main");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("driver_id").IsRequired();
            builder.Property(x => x.FirstName).HasColumnName("first_name");
            builder.Property(x => x.IsVacation).HasColumnName("is_vacation").HasDefaultValue(false);
            builder.Property(x => x.LocomotiveId).HasColumnName("locomotive_id");

            builder.HasOne(x => x.Locomotive).WithMany(x => x.Drivers).HasForeignKey(x => x.LocomotiveId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}