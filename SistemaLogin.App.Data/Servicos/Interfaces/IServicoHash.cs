namespace SistemaLogin.App.Data.Servicos.Interfaces
{
    public interface IServicoHash
    {
        bool CompararHash(string hash, string texto);
        string GerarHash(string texto);
    }
}
