using SistemaLogin.App.Data.Servicos.Interfaces;

using System.Security.Cryptography;
using System.Text;

namespace SistemaLogin.App.Data.Servicos
{
    public class ServicoHash : IServicoHash
    {
        public bool CompararHash(string hash, string texto)
        {
            return GerarHash(texto) == hash;
        }

        public string GerarHash(string texto)
        {
            var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(texto));

            var hash = string.Empty;

            foreach (var hashByte in hashBytes)
                hash += hashByte.ToString("x2");

            return hash;
        }
    }
}
