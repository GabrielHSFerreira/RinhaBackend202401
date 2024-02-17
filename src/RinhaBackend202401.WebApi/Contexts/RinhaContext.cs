using Microsoft.EntityFrameworkCore;
using RinhaBackend202401.WebApi.Configurations;
using RinhaBackend202401.WebApi.Models;

namespace RinhaBackend202401.WebApi.Contexts
{
    public class RinhaContext : DbContext
    {
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Transacao> Transacoes => Set<Transacao>();

        public RinhaContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
        }
    }
}