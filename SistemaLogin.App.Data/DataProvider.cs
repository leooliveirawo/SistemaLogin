using Microsoft.EntityFrameworkCore;

using SistemaLogin.App.Data.Servicos;
using SistemaLogin.App.Data.Servicos.Interfaces;

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

        public IServicoUsuarios ObterServicoUsuarios()
        {
            return new ServicoUsuarios(dbContext, new ServicoHash(), new ProvedorAes());
        }
    }
}
