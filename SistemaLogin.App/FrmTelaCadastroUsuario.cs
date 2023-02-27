using WZSISTEMAS.Data.Autenticacao;
using WZSISTEMAS.Data.Autenticacao.Interfaces;

namespace SistemaLogin.App
{
    public partial class FrmTelaCadastroUsuario : Form
    {
        public enum Modos
        {
            Padrao,
            Visualizar,
            Editar,
            Criar
        }

        private readonly IServicoUsuarios<Usuario> servicoUsuarios;
        private long id;
        private Modos modoAtual;

        public FrmTelaCadastroUsuario()
        {
            InitializeComponent();

            servicoUsuarios = Program.DataProvider.ObterServicoUsuarios();
        }

        private void RecarregarUsuarios()
        {
            var usuarios = servicoUsuarios.ObterTudo();

            dgvUsuarios.Rows.Clear();

            foreach (var usuario in usuarios)
                dgvUsuarios.Rows.Add(usuario.Id, usuario.NomeUsuario, usuario.Email);

            if (dgvUsuarios.Rows.Count > 0)
            {
                dgvUsuarios.Rows[dgvUsuarios.Rows.GetLastRow(DataGridViewElementStates.Visible)].Selected = true;
            }
        }

        private void DefinirModoAtual(Modos modoAtual)
        {
            this.modoAtual = modoAtual;

            if (modoAtual == Modos.Padrao)
            {
                chbxAlterarSenha.Checked = true;
                chbxAlterarSenha.Visible = false;

                btnExcluir.Visible = false;
                btnNovoEditarSalvar.Text = "Novo";
                gbxDados.Enabled = false;
            }
            else if (modoAtual == Modos.Editar)
            {
                chbxAlterarSenha.Checked = false;
                chbxAlterarSenha.Visible = true;

                btnExcluir.Visible = false;
                btnNovoEditarSalvar.Text = "Salvar";
                gbxDados.Enabled = true;
            }
            else if (modoAtual == Modos.Criar)
            {
                chbxAlterarSenha.Checked = true;
                chbxAlterarSenha.Visible = false;

                btnExcluir.Visible = false;
                btnNovoEditarSalvar.Text = "Salvar";
                gbxDados.Enabled = true;
            }
            else if (modoAtual == Modos.Visualizar)
            {
                chbxAlterarSenha.Checked = false;
                chbxAlterarSenha.Visible = false;

                btnExcluir.Visible = true;
                btnNovoEditarSalvar.Text = "Editar";
                gbxDados.Enabled = false;

            }
        }

        private void FrmTelaCadastroUsuario_Load(object sender, EventArgs e)
        {
            DefinirModoAtual(Modos.Padrao);

            try
            {
                RecarregarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void chbxAlterarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.Enabled = chbxAlterarSenha.Checked;
        }

        private void btnNovoEditarSalvar_Click(object sender, EventArgs e)
        {
            if (modoAtual == Modos.Padrao)
            {
                DefinirModoAtual(Modos.Criar);
            }
            else if (modoAtual == Modos.Visualizar)
            {
                DefinirModoAtual(Modos.Editar);
            }
            else
            {
                try
                {
                    if (id == 0)
                    {
                        var usuario = new Usuario()
                        {
                            NomeUsuario = txtNomeUsuario.Text,
                            Email = txtEmail.Text,
                        };

                        servicoUsuarios.Criar(usuario, txtSenha.Text);

                        DefinirModoAtual(Modos.Padrao);

                        txtNomeUsuario.Clear();
                        txtSenha.Clear();
                    }
                    else
                    {
                        var usuario = servicoUsuarios.ObterPorId(id);

                        if (usuario is null)
                        {
                            MessageBox.Show(this, "O usuário não foi encontrado.");

                            return;
                        }

                        usuario.NomeUsuario = txtNomeUsuario.Text;
                        usuario.Email = txtEmail.Text;

                        if (chbxAlterarSenha.Checked)
                            servicoUsuarios.AlterarSenha(usuario, txtSenha.Text);
                        else
                            servicoUsuarios.Alterar(usuario);

                        DefinirModoAtual(Modos.Visualizar);

                        txtSenha.Clear();
                    }

                    RecarregarUsuarios();

                    MessageBox.Show(this, "Usuário salvo com sucesso.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (modoAtual == Modos.Criar)
            {
                txtNomeUsuario.Clear();
                txtEmail.Clear();

                DefinirModoAtual(Modos.Padrao);
            }
            else if (modoAtual == Modos.Editar)
            {
                try
                {
                    var usuario = servicoUsuarios.ObterPorId(id);

                    if (usuario is null)
                    {
                        id = 0;
                        txtNomeUsuario.Clear();
                        txtEmail.Clear();

                        DefinirModoAtual(Modos.Padrao);

                        RecarregarUsuarios();

                        MessageBox.Show(this, "O usuário não foi encontrado.");

                        return;
                    }

                    txtNomeUsuario.Text = usuario.NomeUsuario;
                    txtEmail.Text = usuario.Email;

                    DefinirModoAtual(Modos.Visualizar);

                    RecarregarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
            }
            else if (modoAtual == Modos.Visualizar)
            {
                id = 0;

                txtNomeUsuario.Clear();
                txtEmail.Clear();

                DefinirModoAtual(Modos.Padrao);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(this, "Deseja realmente excluir o usuário?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    servicoUsuarios.Excluir(id);

                    id = 0;
                    txtNomeUsuario.Clear();
                    txtEmail.Clear();

                    DefinirModoAtual(Modos.Padrao);

                    RecarregarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    var id = (long)dgvUsuarios[0, e.RowIndex].Value;

                    var usuario = servicoUsuarios.ObterPorId(id);

                    if (usuario is null)
                    {
                        MessageBox.Show(this, "O usuário não foi encontrado.");

                        return;
                    }

                    this.id = id;

                    txtNomeUsuario.Text = usuario.NomeUsuario;
                    txtEmail.Text = usuario.Email;

                    DefinirModoAtual(Modos.Visualizar);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message);
                }
            }
        }
    }
}
