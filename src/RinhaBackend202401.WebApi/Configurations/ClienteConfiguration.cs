using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RinhaBackend202401.WebApi.Models;

namespace RinhaBackend202401.WebApi.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(x => x.Limite)
                .IsRequired();
            builder.Property(x => x.Saldo)
                .IsRequired();
            builder.Property(x => x.Version)
                .IsRowVersion()
                .IsRequired();

            builder.HasData(
                new Cliente(1, 100000, 0),
                new Cliente(2, 80000, 0),
                new Cliente(3, 1000000, 0),
                new Cliente(4, 10000000, 0),
                new Cliente(5, 500000, 0));
        }
    }
}