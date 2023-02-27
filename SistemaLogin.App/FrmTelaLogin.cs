using WZSISTEMAS.Data.Autenticacao;
using WZSISTEMAS.Data.Autenticacao.Interfaces;

namespace SistemaLogin.App
{
    public partial class FrmTelaLogin : Form
    {
        private readonly IServicoUsuarios<Usuario> servicoUsuarios;

        public FrmTelaLogin()
        {
            InitializeComponent();

            servicoUsuarios = Program.DataProvider.ObterServicoUsuarios();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var frm = new FrmTelaCadastroUsuario();

            frm.ShowDialog(this);
        }

        private void FrmTelaLogin_Load(object sender, EventArgs e)
        {
            menuStrip1.Visible = false;

            txtNomeUsuario.Focus();
            txtNomeUsuario.SelectAll();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                var token = servicoUsuarios.Autenticar(txtNomeUsuario.Text, txtSenha.Text);

                panel1.Visible = false;
                menuStrip1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            menuStrip1.Visible = false;

            txtNomeUsuario.Focus();
            txtNomeUsuario.SelectAll();
        }
    }
}