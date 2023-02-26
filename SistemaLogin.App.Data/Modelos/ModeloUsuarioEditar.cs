namespace SistemaLogin.App.Data.Modelos
{
    public class ModeloUsuarioEditar
    {
        public long Id { get; set; }
        public string NomeUsuario { get; set; } = null!;
        public bool AlterarSenha { get; set; }
        public string? Senha { get; set; }
    }
}
