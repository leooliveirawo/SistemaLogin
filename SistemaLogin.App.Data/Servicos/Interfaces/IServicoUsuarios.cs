using SistemaLogin.App.Data.Entidades;

namespace SistemaLogin.App.Data.Servicos.Interfaces
{
    public interface IServicoUsuarios
    {
        void Alterar(Usuario usuario);
        void AlterarSenha(long id, string senha);
        void Criar(Usuario usuario, string senha);
        void Excluir(long id);
        string GerarNovoToken(string token);
        string Login(string nomeUsuario, string senha);
        Usuario? ObterPorId(long id, bool somenteLeitura = true);
        IEnumerable<Usuario> ObterTudo();
        bool VerificarUsuarioCadastrado();
        bool VerificarToken(string token);
    }
}
