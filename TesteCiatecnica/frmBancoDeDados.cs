using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using TesteCiatecnica.DBConnection;

namespace TesteCiatecnica
{
    public partial class frmBancoDeDados : Form
    {
        public frmBancoDeDados()
        {
            InitializeComponent();
            txtProtocol.Text = Properties.Settings.Default.DB_Protocol;
            txtPort.Text = Properties.Settings.Default.DB_Port;
            txtServiceName.Text = Properties.Settings.Default.DB_ServiceName;
            txtUserId.Text = Properties.Settings.Default.DB_UserID;
            txtPassword.Text = Properties.Settings.Default.DB_Password;
            txtHost.Text = Properties.Settings.Default.DB_Host;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DB_Protocol = txtProtocol.Text;
            Properties.Settings.Default.DB_Port = txtPort.Text;
            Properties.Settings.Default.DB_ServiceName = txtServiceName.Text;
            Properties.Settings.Default.DB_UserID = txtUserId.Text;
            Properties.Settings.Default.DB_Password = txtPassword.Text;
            Properties.Settings.Default.DB_Host = txtHost.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Conexão salva com sucesso!\r\n Teste se a conexão clicando no botão Testar Conexão.","Save sucessfully");
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
           
            TestConnection();
        }
        private void TestConnection()
        {
            if (!DataBaseConnection.ConnectValidation().Status)
            {
                MessageBox.Show(DataBaseConnection.ConnectValidation().Message, "Returned Error");
                return;
            }
            else
            {
                MessageBox.Show(DataBaseConnection.ConnectValidation().Message, "Sucessfully");
            }
        }
    }
}
