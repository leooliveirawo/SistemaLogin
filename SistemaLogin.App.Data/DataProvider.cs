using Microsoft.EntityFrameworkCore;

using WZSISTEMAS.Data.Autenticacao;
using WZSISTEMAS.Data.Autenticacao.Interfaces;
using WZSISTEMAS.Data.Criptografia;

namespace SistemaLogin.App.Data
{
    public class DataProvider
    {
        private readonly DbContext dbContext;

        public DataProvider()
        {
            dbContext = new SistemaLoginDbContext();
        }

        public IServicoUsuarios<Usuario> ObterServicoUsuarios()
        {
            return new ServicoUsuarios<Usuario>(
                new RepositorioUsuarios(dbContext),
                new ProvedorSHA256(),
                new ProvedorAes(),
                new DadosCriptografiaAutenticacao
                {
                    Chave = "12345678123456781234567812345678",
                    IV = "1234567812345678"
                });
        }
    }
}
