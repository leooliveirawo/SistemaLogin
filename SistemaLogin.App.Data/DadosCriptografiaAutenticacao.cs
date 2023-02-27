using WZSISTEMAS.Data.Autenticacao.Interfaces;

namespace SistemaLogin.App.Data
{
    public class DadosCriptografiaAutenticacao : IDadosCriptografiaAutenticacao
    {
        public string Chave { get; init; } = null!;

        public string IV { get; init; } = null!;
    }
}
