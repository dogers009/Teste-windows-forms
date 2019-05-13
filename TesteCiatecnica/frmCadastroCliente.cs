using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteCiatecnica.Core;
using TesteCiatecnica.DBConnection;
using TesteCiatecnica.Pessoa;
using TesteCiatecnica.Result;

namespace TesteCiatecnica
{
    public partial class frmCadastro : Form
    {
        Corebridge coreBridge;
        ExecutionResult execRes;
        private int idPessoa;
        public frmCadastro()
        {

            coreBridge = new Corebridge();
            execRes = new ExecutionResult();
            InitializeComponent();
        }

     
        private void frmCadastro_Load(object sender, EventArgs e)
        {
            rdPessoaFisica.Checked=true;
        }

        private void lblCNPJ_Click(object sender, EventArgs e)
        {

        }

        private void rdPessoaFisica_CheckedChanged(object sender, EventArgs e)
        {
            CheckPessoaFisica();
            lblFiltro.Text = "CPF: ";
            dgvPessoa.DataSource = null;
        }      

        private void rdPessoaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            CheckPessoaJuridica();
            lblFiltro.Text = "CNPJ: ";
            dgvPessoa.DataSource = null;
        }
        private void CheckPessoaFisica()
        {

            //hide pessoa juridica
            lblCNPJ.Visible = false;
            lblRazaoSocial.Visible = false;
            lblNomeFantasia.Visible = false;
            maskedCNPJ.Visible = false;
            txtRazaoSocial.Visible = false;
            txtNomeFantasia.Visible = false;

            //show pessoa juridica
            lblCPF.Visible = true;
            maskedCPF.Visible = true;
            lblNome.Visible = true;
            txtNome.Visible = true;
            lblSobrenome.Visible = true;
            txtSobrenome.Visible = true;
            lblDataNascimento.Visible = true;
            dateTimeNascimento.Visible = true;
            maskedCPF.Focus();



        }
        private void CheckPessoaJuridica()
        {

            //Show pessoa juridica
            lblCNPJ.Visible = true;
            lblRazaoSocial.Visible = true;
            lblNomeFantasia.Visible = true;
            maskedCNPJ.Visible = true;
            txtRazaoSocial.Visible = true;
            txtNomeFantasia.Visible = true;

            //Hide pessoa fisica
            lblCPF.Visible = false;
            maskedCPF.Visible = false;
            lblNome.Visible = false;
            txtNome.Visible = false;
            lblSobrenome.Visible = false;
            txtSobrenome.Visible = false;
            lblDataNascimento.Visible = false;
            dateTimeNascimento.Visible = false;

            maskedCNPJ.Focus();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
           if(rdPessoaFisica.Checked)
           {
                InsertPessoaFisica();
                return;
           }
           else
           {
                InsertPessoaJuridica();
                return;
           }
        }
        private PessoaFisica PessoaFisica()
        {
            return new PessoaFisica
            {
               CPF = maskedCPF.Text,
               Data_de_nascimento = dateTimeNascimento.Value,
               Nome = txtNome.Text,
               Sobrenome = txtSobrenome.Text,
               CEP = txtCEP.Text,
               Logradouro = txtLogradouro.Text,
               Numero = (int)numericNumero.Value,
               Complemento = txtComplemento.Text,
               Bairro = txtBairro.Text,
               Cidade = txtCidade.Text,
               UF = txtUF.Text
            };
        }
        private PessoaJuridica PessoaJuridica()
        {
            return new PessoaJuridica
            {
                CNPJ = maskedCNPJ.Text,
                Razao_social = txtRazaoSocial.Text,
                Nome_fantasia = txtNomeFantasia.Text,
                CEP = txtCEP.Text,
                Logradouro = txtLogradouro.Text,
                Numero = (int)numericNumero.Value,
                Complemento = txtComplemento.Text,
                Bairro = txtBairro.Text,
                Cidade = txtCidade.Text,
                UF = txtUF.Text
            };
        }
        
        private void InsertPessoaJuridica()
        {            
            PessoaJuridica pessoaJuridica = PessoaJuridica();
            execRes = coreBridge.InsertPessoaJuridica(pessoaJuridica);
            CheckExecResult(execRes,nameof(InsertPessoaJuridica));                     
        }
        private void InsertPessoaFisica()
        {
            PessoaFisica pessoaFisica = PessoaFisica();
            execRes = coreBridge.InsertPessoaFisica(pessoaFisica);
            CheckExecResult(execRes,nameof(InsertPessoaJuridica));           
        }
        private void DeletePessoaJuridica()
        {
            
            DialogResult dialogResult = MessageBox.Show("Você tem certeza que deseja deletar o cliente selecionado?", "Sure delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                PessoaJuridica pessoaJuridica = PessoaJuridica();
                execRes = coreBridge.DeletePessoaJuridicaData(idPessoa);
                CheckExecResult(execRes,nameof(DeletePessoaJuridica));
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!", "Cancel operation");
            }
        }
        private void DeletePessoaFisica()
        {
            DialogResult dialogResult = MessageBox.Show("Você tem certeza que deseja deletar o cliente selecionado?", "Sure delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                PessoaFisica pessoaFisica = PessoaFisica();
                execRes = coreBridge.DeletePessoaFisica(idPessoa);
                CheckExecResult(execRes,nameof(DeletePessoaFisica));
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!", "Cancel operation");
            }
        }
        private void UpdatePessoaJuridica()
        {
            DialogResult dialogResult = MessageBox.Show("Você tem certeza que deseja atualizar o cliente selecionado?", "Sure update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                PessoaJuridica pessoaJuridica = PessoaJuridica();
                execRes = coreBridge.UpdatePessoaJuridicaData(pessoaJuridica, idPessoa);
                CheckExecResult(execRes,nameof(UpdatePessoaJuridica));
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!","Cancel operation");                
            }
            
        }
        private void UpdatePessoaFisica()
        {
            DialogResult dialogResult = MessageBox.Show("Você tem certeza que deseja atualizar o cliente selecionado?", "Sure update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                PessoaFisica pessoaFisica = PessoaFisica();
                execRes = coreBridge.UpdatePessoaFisica(pessoaFisica,idPessoa);
                CheckExecResult(execRes,nameof(UpdatePessoaJuridica));
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("Operação cancelada!", "Cancel operation");
            }
        }
        private void ClearTextBox()
        {
            foreach (var c in groupBoxPessoaFisica.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if (c is MaskedTextBox)
                {
                    ((MaskedTextBox)c).Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBox();
            dgvPessoa.DataSource = null;
        }     
        private void configurarBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBancoDeDados frmBd = new frmBancoDeDados();
            frmBd.ShowDialog();
        }
        private void CheckExecResult(ExecutionResult execRes,string operation)
        {
            if (!execRes.Status)
            {
                MessageBox.Show(execRes.Message, "Returned Error");
                return;
            }
            else
            {
                if (operation != "txtFilter")
                {
                    MessageBox.Show(execRes.Message, "Sucessfully");
                    dgvPessoa.DataSource = execRes.Anything;
                    dgvPessoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    idPessoa = 0;
                    ClearTextBox();
                }
                else
                {
                    dgvPessoa.DataSource = execRes.Anything;
                    idPessoa = 0;
                    dgvPessoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                return;
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (idPessoa == 0)
            {
                MessageBox.Show("Por favor selecione o cliente clicando na grid!", "Error");
                return;
            }
            if (rdPessoaFisica.Checked)
            {
                DeletePessoaFisica();
            }
            else
            {
                DeletePessoaJuridica();
            }
        }

        private void dgvPessoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                idPessoa = Convert.ToInt16(dgvPessoa.CurrentRow.Cells[0].Value.ToString());
                if (rdPessoaFisica.Checked)
                {
                    maskedCPF.Text = dgvPessoa.CurrentRow.Cells[1].Value.ToString();
                    dateTimeNascimento.Value = Convert.ToDateTime(dgvPessoa.CurrentRow.Cells[2].Value.ToString());
                    txtNome.Text = dgvPessoa.CurrentRow.Cells[3].Value.ToString();
                    txtSobrenome.Text = dgvPessoa.CurrentRow.Cells[4].Value.ToString();
                    txtCEP.Text = dgvPessoa.CurrentRow.Cells[5].Value.ToString();                   
                    txtLogradouro.Text = dgvPessoa.CurrentRow.Cells[6].Value.ToString();
                    numericNumero.Value = Convert.ToInt16(dgvPessoa.CurrentRow.Cells[7].Value.ToString());
                    txtComplemento.Text = dgvPessoa.CurrentRow.Cells[8].Value.ToString();
                    txtBairro.Text = dgvPessoa.CurrentRow.Cells[9].Value.ToString();
                    txtCidade.Text = dgvPessoa.CurrentRow.Cells[10].Value.ToString();
                    txtUF.Text = dgvPessoa.CurrentRow.Cells[11].Value.ToString();
                }
                else
                {
                    maskedCNPJ.Text = dgvPessoa.CurrentRow.Cells[1].Value.ToString();
                    txtRazaoSocial.Text = dgvPessoa.CurrentRow.Cells[2].Value.ToString();
                    txtNomeFantasia.Text = dgvPessoa.CurrentRow.Cells[3].Value.ToString();
                    txtCEP.Text = dgvPessoa.CurrentRow.Cells[4].Value.ToString();
                    txtLogradouro.Text = dgvPessoa.CurrentRow.Cells[5].Value.ToString();
                    numericNumero.Value = Convert.ToInt16(dgvPessoa.CurrentRow.Cells[6].Value.ToString());
                    txtComplemento.Text = dgvPessoa.CurrentRow.Cells[7].Value.ToString();
                    txtBairro.Text = dgvPessoa.CurrentRow.Cells[8].Value.ToString();
                    txtCidade.Text = dgvPessoa.CurrentRow.Cells[9].Value.ToString();
                    txtUF.Text = dgvPessoa.CurrentRow.Cells[10].Value.ToString();
                }
            }
           
        }

        private void txtUpdate_Click(object sender, EventArgs e)
        {
            if (idPessoa == 0)
            {
                MessageBox.Show("Por favor selecione o cliente clicando na grid!", "Error");
                return;
            }
            if (rdPessoaFisica.Checked)
            {
                UpdatePessoaFisica();
            }
            else
            {
                UpdatePessoaJuridica();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if (rdPessoaFisica.Checked)
            {
                execRes = coreBridge.FilterPessoaFisica(txtFilter.Text);
                CheckExecResult(execRes, nameof(txtFilter));
            }
            else
            {
                execRes = coreBridge.FilterPessoaJuridica(txtFilter.Text);
                CheckExecResult(execRes, nameof(txtFilter));
            }
        }
    }
}
