using SistemaLogin.App.Data;
using SistemaLogin.App.Data.Servicos;

namespace SistemaLogin.App
{
    internal static class Program
    {
        public static DataProvider DataProvider { get; } = new DataProvider();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            while (true)
            {
                if (!DataProvider.ObterServicoUsuarios().VerificarUsuarioCadastrado())
                {
                    if (MessageBox.Show("É necessário criar um novo usuario. Deseja criar?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Application.Run(new FrmTelaCadastroUsuario());
                    else
                    {
                        MessageBox.Show("O sitema será fechado.");
                        break;
                    }
                }
                else
                {
                    Application.Run(new FrmTelaLogin());
                    break;
                }
            }
        }
    }
}