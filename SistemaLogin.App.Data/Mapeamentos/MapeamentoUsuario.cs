using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using WZSISTEMAS.Data.Autenticacao;

namespace SistemaLogin.App.Data.Mapeamentos
{
    public class MapeamentoUsuario : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIOS");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.NomeUsuario)
                .IsUnique();

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnType("BIGINT")
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(x => x.NomeUsuario)
                .HasColumnName("NOME_USUARIO")
                .HasColumnType("VARCHAR")
                .HasMaxLength(35)
                .IsRequired();

            builder.Property(x => x.HashSenha)
                .HasColumnName("HASH_SENHA")
                .HasColumnType("VARCHAR")
                .HasMaxLength(135)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
