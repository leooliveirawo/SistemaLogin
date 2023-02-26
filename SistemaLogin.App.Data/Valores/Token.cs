namespace SistemaLogin.App.Data.Valores
{
    [Serializable]
    public class Token
    {
        public string NomeUsuario { get; set; } = null!;
        public DateTime LogadoEm { get; set; }
        public DateTime ExpiraEm { get; set; }
    }
}
