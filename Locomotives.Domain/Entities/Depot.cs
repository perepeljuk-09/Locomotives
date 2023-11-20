using Locomotives.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locomotives.Domain.Entities
{
    public class Depot
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Locomotive>? Locomotives { get; set; } = new();
    }
}
public class DepotConfiguration : IEntityTypeConfiguration<Depot>
{
    void IEntityTypeConfiguration<Depot>.Configure(EntityTypeBuilder<Depot> builder)
    {
        builder.ToTable("depots", "main");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("depot_id");
        builder.Property(x => x.Name).HasColumnName("depot_name");

    }
}