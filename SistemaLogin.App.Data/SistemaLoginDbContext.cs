using Microsoft.EntityFrameworkCore;

using SistemaLogin.App.Data.Mapeamentos;

using WZSISTEMAS.Data.Autenticacao;

namespace SistemaLogin.App.Data
{
    public class SistemaLoginDbContext : DbContext
    {
        public static string SistemaLoginConnectionString { get; set; } = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SistemaLogin;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(SistemaLoginConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MapeamentoUsuario());
        }
    }
}
