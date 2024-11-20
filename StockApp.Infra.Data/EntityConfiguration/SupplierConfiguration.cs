using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Infra.Data.EntityConfiguration
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("Suppliers");
            builder.HasKey(t => t.Id);
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.Property(s => s.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.Property(s => s.ContactEmail).HasMaxLength(100).IsRequired();
        }
    }
}
