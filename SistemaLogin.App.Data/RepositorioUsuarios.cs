using Microsoft.EntityFrameworkCore;

using WZSISTEMAS.Data.Autenticacao;
using WZSISTEMAS.Data.Autenticacao.Interfaces;

namespace SistemaLogin.App.Data
{
    public class RepositorioUsuarios : IRepositorioUsuarios<Usuario>
    {
        private readonly DbContext dbContext;

        public RepositorioUsuarios(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual void Alterar(Usuario usuario)
        {
            dbContext.Update(usuario);
            dbContext.SaveChanges();
        }

        public virtual void Criar(Usuario usuario)
        {
            dbContext.Add(usuario);
            dbContext.SaveChanges();
        }

        public virtual void Excluir(long id)
        {
            dbContext.Remove(dbContext.Find<Usuario>(id) ?? throw new InvalidOperationException("O usuário não foi encontrado"));
            dbContext.SaveChanges();
        }

        public virtual string ObterHashChaveMestre(string nomeUsuario)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Where(x => x.NomeUsuario == nomeUsuario).Select(x => x.HashChaveMestre).FirstOrDefault() ?? string.Empty;
        }

        public virtual string ObterNomeUsuarioPorId(long id)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Where(x => x.Id == id).Select(x => x.NomeUsuario).FirstOrDefault() ?? string.Empty;
        }

        public virtual Usuario? ObterPorId(long id)
        {
            return dbContext.Set<Usuario>().FirstOrDefault(x => x.Id == id);
        }

        public virtual IEnumerable<Usuario> ObterTudo()
        {
            return dbContext.Set<Usuario>().AsNoTracking().ToList();
        }

        public virtual bool VerificarHashChaveMestre(string nomeUsuario, string hashChaveMestre)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Where(x => x.NomeUsuario == nomeUsuario && x.HashChaveMestre == hashChaveMestre).Select(x => x.Id).Any();
        }

        public virtual bool VerificarNomeUsuarioEHashSenha(string nomeUsuario, string hashSenha)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Where(x => x.NomeUsuario == nomeUsuario && x.HashSenha == hashSenha).Select(x => x.Id).Any();
        }

        public virtual bool VerificarNomeUsuarioUsado(string nomeUsuario)
        {
            return dbContext.Set<Usuario>().AsNoTracking().Where(x => x.NomeUsuario == nomeUsuario).Select(x => x.Id).Any();
        }

        public virtual bool VerificarUsuarioExiste()
        {
            return dbContext.Set<Usuario>().AsNoTracking().Select(x => x.Id).Any();
        }
    }
}
