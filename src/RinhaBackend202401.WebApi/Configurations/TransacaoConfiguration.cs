using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RinhaBackend202401.WebApi.Models;

namespace RinhaBackend202401.WebApi.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.Property(x => x.Tipo)
                .IsRequired();
            builder.Property(x => x.Valor)
                .IsRequired();
            builder.Property(x => x.Descricao)
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(x => x.RealizadaEm)
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Transacoes)
                .HasForeignKey(x => x.IdCliente)
                .IsRequired();
        }
    }
}