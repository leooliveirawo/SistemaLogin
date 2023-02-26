using Microsoft.EntityFrameworkCore;

using SistemaLogin.App.Data.Entidades;
using SistemaLogin.App.Data.Servicos.Interfaces;
using SistemaLogin.App.Data.Valores;

using System.Security;
using System.Text.Json;

namespace SistemaLogin.App.Data.Servicos
{
    public class ServicoUsuarios : IServicoUsuarios
    {
        private const string chaveCriptografia = "1234567812345678";

        private readonly DbContext dbContext;
        private readonly IServicoHash servicoHash;
        private readonly IServicoCriptografia servicoCriptografia;

        public ServicoUsuarios(DbContext dbContext, IServicoHash servicoHash, IServicoCriptografia servicoCriptografia)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.servicoHash = servicoHash ?? throw new ArgumentNullException(nameof(servicoHash));
            this.servicoCriptografia = servicoCriptografia ?? throw new ArgumentNullException(nameof(servicoCriptografia));
        }

        public void Alterar(Usuario usuario)
        {
            if (usuario is null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.NomeUsuario))
                throw new InvalidOperationException("O nome de usuário não foi informado");

            if (string.IsNullOrWhiteSpace(usuario.HashSenha))
                throw new InvalidOperationException("O hash da senha não foi informado");

            dbContext.Update(usuario);
            dbContext.SaveChanges();
        }

        public void AlterarSenha(long id, string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new InvalidOperationException("A senha não foi informada");

            var usuario = dbContext.Find<Usuario>(id);

            if (usuario is null)
                throw new InvalidOperationException($"O usuário de Id \"{id}\" não foi encontrado");

            usuario.HashSenha = servicoHash.GerarHash(senha);

            dbContext.Update(usuario);
            dbContext.SaveChanges();
        }

        public void Criar(Usuario usuario, string senha)
        {
            if (usuario is null)
                throw new ArgumentNullException(nameof(usuario));

            if (string.IsNullOrWhiteSpace(usuario.NomeUsuario))
                throw new InvalidOperationException("O nome de usuário não foi informado");

            if (string.IsNullOrWhiteSpace(senha))
                throw new InvalidOperationException("A senha não foi informada");

            usuario.HashSenha = servicoHash.GerarHash(senha);

            dbContext.Add(usuario);
            dbContext.SaveChanges();
        }

        public void Excluir(long id)
        {
            var usuario = dbContext.Find<Usuario>(id);

            if (usuario is null)
                throw new InvalidOperationException($"O usuário de Id \"{id}\" não foi encontrado");

            dbContext.Remove(usuario);
            dbContext.SaveChanges();
        }

        public string GerarNovoToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("O token não é válido");

            if (VerificarToken(token))
            {
                var tokenDescriptografado = servicoCriptografia.Descriptografar(chaveCriptografia, token);

                var tokenI = JsonSerializer.Deserialize<Token>(tokenDescriptografado);

                if (tokenI is null)
                    throw new InvalidOperationException("O token expirou");

                var novoTokenI = new Token
                {
                    NomeUsuario = tokenI.NomeUsuario,
                    LogadoEm = DateTime.UtcNow,
                    ExpiraEm = DateTime.UtcNow.AddMinutes(15),
                };

                var novoToken = JsonSerializer.Serialize(novoTokenI);

                return servicoCriptografia.Criptografar(chaveCriptografia, novoToken);
            }

            throw new InvalidOperationException("O token expirou");
        }

        public string Login(string nomeUsuario, string senha)
        {
            if (string.IsNullOrWhiteSpace(nomeUsuario))
                throw new InvalidOperationException("O nome de usuário não foi informado");

            if (string.IsNullOrWhiteSpace(senha))
                throw new InvalidOperationException("A senha não foi informada");

            var hashSenha = servicoHash.GerarHash(senha);

            if (dbContext.Set<Usuario>().AsNoTracking().Where(x => x.HashSenha == hashSenha).Select(x => x.Id).Any())
            {
                var token = new Token
                {
                    LogadoEm = DateTime.UtcNow,
                    ExpiraEm = DateTime.UtcNow.AddDays(15),
                    NomeUsuario = nomeUsuario
                };

                var texto = JsonSerializer.Serialize(token);

                return servicoCriptografia.Criptografar(chaveCriptografia, JsonSerializer.Serialize(token));
            }

            throw new SecurityException("O nome de usuário e senha não são válidos");
        }

        public Usuario? ObterPorId(long id, bool somenteLeitura = true)
        {
            if (somenteLeitura)
                return dbContext.Set<Usuario>().AsNoTracking().FirstOrDefault(x => x.Id == id);
            else
                return dbContext.Set<Usuario>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Usuario> ObterTudo()
        {
            return dbContext.Set<Usuario>().AsNoTracking().ToList();
        }

        public bool VerificarToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new InvalidOperationException("O token não é válido");

            var tokenDescriptografado = servicoCriptografia.Descriptografar(chaveCriptografia, token);

            var tokenI = JsonSerializer.Deserialize<Token>(tokenDescriptografado);

            if (tokenI is null)
                throw new InvalidOperationException("O token não é válido");

            if (dbContext.Set<Usuario>().AsNoTracking().Where(x => x.NomeUsuario == tokenI.NomeUsuario).Select(x => x.Id).Any())
            {
                if (tokenI.ExpiraEm < DateTime.Now)
                    return false;

                return true;
            }

            return false;
        }

        public bool VerificarUsuarioCadastrado()
        {
            return dbContext.Set<Usuario>().Select(x => x.Id).Any();
        }
    }
}
