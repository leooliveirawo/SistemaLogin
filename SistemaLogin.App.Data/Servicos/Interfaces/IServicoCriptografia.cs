namespace SistemaLogin.App.Data.Servicos.Interfaces
{
    public interface IServicoCriptografia
    {
        string Criptografar(string chave, string texto);
        string Descriptografar(string chave, string textoCriptografado);
    }
}
