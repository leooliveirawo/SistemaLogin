using SistemaLogin.App.Data.Servicos.Interfaces;

using System.Security.Cryptography;
using System.Text;

namespace SistemaLogin.App.Data.Servicos
{
    public class ServicoCriptografia : IServicoCriptografia
    {
        private const string iv = "1234567812345678";

        public string Criptografar(string chave, string texto)
        {
            using var aes = Aes.Create();

            aes.IV = Encoding.UTF8.GetBytes(iv);
            aes.Key = Encoding.UTF8.GetBytes(chave);

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            var textoBytes = Encoding.UTF8.GetBytes(texto);

            cryptoStream.Write(textoBytes, 0, textoBytes.Length);
            cryptoStream.FlushFinalBlock();

            var textoCritptografadoBytes = memoryStream.ToArray();

            return Convert.ToBase64String(textoCritptografadoBytes);
        }

        public string Descriptografar(string chave, string textoCriptografado)
        {
            using var aes = Aes.Create();

            aes.IV = Encoding.UTF8.GetBytes(iv);
            aes.Key = Encoding.UTF8.GetBytes(chave);

            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

            var textoBytes = Convert.FromBase64String(textoCriptografado);

            cryptoStream.Write(textoBytes, 0, textoBytes.Length);
            cryptoStream.FlushFinalBlock();

            var textoDescritptografadoBytes = memoryStream.ToArray();

            return Encoding.UTF8.GetString(textoDescritptografadoBytes);
        }
    }
}
