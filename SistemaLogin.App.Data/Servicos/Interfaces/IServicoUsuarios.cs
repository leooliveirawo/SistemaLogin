using SistemaLogin.App.Data.Entidades;
using SistemaLogin.App.Data.Modelos;

namespace SistemaLogin.App.Data.Servicos.Interfaces
{
    public interface IServicoUsuarios
    {
        void Alterar(ModeloUsuarioEditar modelo);
        void Criar(ModeloUsuarioCriar modelo);
        void Excluir(long id);
        string GerarNovoToken(string token);
        string Login(string nomeUsuario, string senha);
        ModeloUsuarioDetalhes? ObterPorId(long id);
        IEnumerable<ModeloUsuarioTabela> ObterTudo();
        bool VerificarUsuarioCadastrado();
        bool VerificarToken(string token);
    }
}
