namespace SistemaLogin.App.Data.Entidades
{
    public class Usuario
    {
        public long Id { get; set; }
        public string NomeUsuario { get; set; } = null!;
        public string HashSenha { get; set; } = null!;
    }
}
